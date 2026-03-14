using Microsoft.Data.SqlClient;

namespace GoTrot.Data
{
    public static class MigrationHelper
    {
        private static string ConnectionString => AppConfiguration.ConnectionString;

        public static void RunAll()
        {
            // Postojeće kolone
            EnsureColumn("Scooters", "ChargingStartTime", "DATETIME2 NULL");
            EnsureColumn("Scooters", "Status", "INT NOT NULL DEFAULT 0");
            EnsureColumn("Users", "IsAdmin", "BIT NOT NULL DEFAULT 0");

            // Nove tabele
            EnsurePaymentsTable();
            EnsureZoneTable();
            EnsureColumn("Scooters", "ZonaId", "INT NULL");
            EnsureServisTable();
            EnsureRatingTable();
            EnsureRezervacijaTable();

            // Seed zona
            SeedZone();
            SeedScooters();
        }

        private static void EnsureColumn(string table, string column, string definition)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            using var checkCmd = connection.CreateCommand();
            checkCmd.CommandText = @"
                SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_NAME = @table AND COLUMN_NAME = @column";
            checkCmd.Parameters.AddWithValue("@table", table);
            checkCmd.Parameters.AddWithValue("@column", column);

            int count = (int)checkCmd.ExecuteScalar()!;

            if (count == 0)
            {
                using var alterCmd = connection.CreateCommand();
                alterCmd.CommandText = $"ALTER TABLE [{table}] ADD [{column}] {definition}";
                alterCmd.ExecuteNonQuery();
            }
        }

        private static void EnsureTable(string tableName, string createSql)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            using var checkCmd = connection.CreateCommand();
            checkCmd.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @name";
            checkCmd.Parameters.AddWithValue("@name", tableName);

            int count = (int)checkCmd.ExecuteScalar()!;
            if (count == 0)
            {
                using var createCmd = connection.CreateCommand();
                createCmd.CommandText = createSql;
                createCmd.ExecuteNonQuery();
            }
        }

        private static void EnsurePaymentsTable() => EnsureTable("Payments", @"
            CREATE TABLE [Payments] (
                [Id] INT IDENTITY(1,1) PRIMARY KEY,
                [UserId] INT NOT NULL,
                [Iznos] DECIMAL(18,2) NOT NULL,
                [VrijemeUplate] DATETIME2 NOT NULL DEFAULT GETDATE(),
                [Napomena] NVARCHAR(500) NOT NULL DEFAULT '',
                CONSTRAINT [FK_Payments_Users] FOREIGN KEY ([UserId])
                    REFERENCES [Users]([Id]) ON DELETE NO ACTION
            )");

        private static void EnsureZoneTable() => EnsureTable("Zone", @"
            CREATE TABLE [Zone] (
                [Id] INT IDENTITY(1,1) PRIMARY KEY,
                [Naziv] NVARCHAR(100) NOT NULL DEFAULT '',
                [Opis] NVARCHAR(500) NOT NULL DEFAULT ''
            )");

        private static void EnsureServisTable() => EnsureTable("Servisi", @"
            CREATE TABLE [Servisi] (
                [Id] INT IDENTITY(1,1) PRIMARY KEY,
                [ScooterId] INT NOT NULL,
                [DatumPocetka] DATETIME2 NOT NULL DEFAULT GETDATE(),
                [DatumZavrsetka] DATETIME2 NULL,
                [Opis] NVARCHAR(500) NOT NULL DEFAULT '',
                CONSTRAINT [FK_Servisi_Scooters] FOREIGN KEY ([ScooterId])
                    REFERENCES [Scooters]([Id]) ON DELETE CASCADE
            )");

        private static void EnsureRatingTable() => EnsureTable("Rati", @"
            CREATE TABLE [Rati] (
                [Id] INT IDENTITY(1,1) PRIMARY KEY,
                [RideId] INT NOT NULL,
                [Ocjena] INT NOT NULL DEFAULT 5,
                [Komentar] NVARCHAR(500) NOT NULL DEFAULT '',
                [VrijemeOcjene] DATETIME2 NOT NULL DEFAULT GETDATE(),
                CONSTRAINT [FK_Rati_Rides] FOREIGN KEY ([RideId])
                    REFERENCES [Rides]([Id]) ON DELETE CASCADE
            )");

        private static void EnsureRezervacijaTable() => EnsureTable("Rezervacije", @"
            CREATE TABLE [Rezervacije] (
                [Id] INT IDENTITY(1,1) PRIMARY KEY,
                [UserId] INT NOT NULL,
                [ScooterId] INT NOT NULL,
                [VrijemeRezervacije] DATETIME2 NOT NULL DEFAULT GETDATE(),
                CONSTRAINT [FK_Rezervacije_Users] FOREIGN KEY ([UserId])
                    REFERENCES [Users]([Id]) ON DELETE CASCADE,
                CONSTRAINT [FK_Rezervacije_Scooters] FOREIGN KEY ([ScooterId])
                    REFERENCES [Scooters]([Id]) ON DELETE CASCADE
            )");

        private static void SeedZone()
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            using var checkCmd = connection.CreateCommand();
            checkCmd.CommandText = "SELECT COUNT(*) FROM [Zone]";
            int count = (int)checkCmd.ExecuteScalar()!;
            if (count > 0) return;

            var zone = new[]
            {
                ("Centar", "Centralna zona — Španjerska ulica, Rondo"),
                ("Stari Grad", "Stari grad — Kujundžiluk, Koski Mehmed-paša džamija"),
                ("Bijelo Polje", "Bijelo Polje — Industrijska zona i rezidencijalni dio"),
                ("Sjever", "Sjeverni dio — Zalik, Brankovac"),
                ("Jug", "Južni dio — Blagaj, Ilići")
            };

            foreach (var (naziv, opis) in zone)
            {
                using var insertCmd = connection.CreateCommand();
                insertCmd.CommandText = "INSERT INTO [Zone] ([Naziv], [Opis]) VALUES (@naziv, @opis)";
                insertCmd.Parameters.AddWithValue("@naziv", naziv);
                insertCmd.Parameters.AddWithValue("@opis", opis);
                insertCmd.ExecuteNonQuery();
            }
        }
        private static void SeedScooters()
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            using var checkCmd = connection.CreateCommand();
            checkCmd.CommandText = "SELECT COUNT(*) FROM [Scooters]";
            int count = (int)checkCmd.ExecuteScalar()!;
            if (count > 0) return;

            // Dohvati ID-eve zona
            var zonaIds = new Dictionary<string, int>();
            using var zonaCmd = connection.CreateCommand();
            zonaCmd.CommandText = "SELECT Id, Naziv FROM [Zone]";
            using var reader = zonaCmd.ExecuteReader();
            while (reader.Read())
                zonaIds[reader.GetString(1)] = reader.GetInt32(0);
            reader.Close();

            int GetZona(string naziv) => zonaIds.TryGetValue(naziv, out var id) ? id : (int?)null ?? 1;

            // Model, BatteryLevel, IsAvailable, PricePerMinute, Location, ZonaNaziv, Status
            var scooters = new[]
            {
                // Centar
                ("Xiaomi Pro 2",      95, true,  0.25m, "Španjerska ulica",         "Centar",      0),
                ("Segway Max G2",     88, true,  0.30m, "Rondo",                    "Centar",      0),
                ("Ninebot E45",       72, true,  0.25m, "Stara čaršija - Rondo",    "Centar",      0),
                ("Xiaomi Essential",  60, true,  0.20m, "Trg dr. Franje Tuđmana",   "Centar",      0),
                ("Segway Ninebot F2", 45, false, 0.25m, "Mostarsko polje",          "Centar",      2), // NaPunjenju

                // Stari Grad
                ("Xiaomi Pro 2",      100, true, 0.25m, "Kujundžiluk",              "Stari Grad",  0),
                ("Ninebot E45",        83, true, 0.25m, "Koski Mehmed-paša džamija","Stari Grad",  0),
                ("Xiaomi Essential",   55, true, 0.20m, "Stari most - lijevа obala","Stari Grad",  0),
                ("Segway Max G2",      30, false, 0.30m,"Croata most",              "Stari Grad",  1), // NedostupanPraznaBaterija

                // Bijelo Polje
                ("Ninebot F2 Plus",   90, true,  0.25m, "Bijelo Polje centar",      "Bijelo Polje",0),
                ("Xiaomi Pro 2",      78, true,  0.25m, "Carinska uprava BP",       "Bijelo Polje",0),
                ("Segway ES4",        65, true,  0.20m, "Pijaca Bijelo Polje",      "Bijelo Polje",0),
                ("Xiaomi Essential",  40, false, 0.20m, "Industrijska zona BP",     "Bijelo Polje",4), // NedostupanZaOdrzavanje

                // Sjever
                ("Segway Max G2",     92, true,  0.30m, "Zalik - glavna ulica",     "Sjever",      0),
                ("Ninebot E45",       80, true,  0.25m, "Brankovac",                "Sjever",      0),
                ("Xiaomi Pro 2",      70, true,  0.25m, "Cernica - centar",         "Sjever",      0),
                ("Xiaomi Essential",  55, true,  0.20m, "Vrapčići",                 "Sjever",      0),
                ("Segway ES4",        20, false, 0.20m, "Zalik - sportski centar",  "Sjever",      2), // NaPunjenju

                // Jug
                ("Ninebot F2 Plus",   97, true,  0.25m, "Blagaj - ulaz u selo",     "Jug",         0),
                ("Xiaomi Pro 2",      85, true,  0.25m, "Ilići - centar",           "Jug",         0),
                ("Segway Max G2",     75, true,  0.30m, "Šehovina",                 "Jug",         0),
                ("Ninebot E45",       50, true,  0.25m, "Sutina",                   "Jug",         0),
                ("Xiaomi Essential",  35, false, 0.20m, "Carina - jug",             "Jug",         1), // NedostupanPraznaBaterija
            };

            foreach (var (model, battery, isAvail, price, location, zonaNaziv, status) in scooters)
            {
                using var insertCmd = connection.CreateCommand();
                insertCmd.CommandText = @"
                    INSERT INTO [Scooters] 
                        ([Model], [BatteryLevel], [IsAvailable], [PricePerMinute], [Location], [ZonaId], [Status])
                    VALUES 
                        (@model, @battery, @isAvail, @price, @location, @zonaId, @status)";
                insertCmd.Parameters.AddWithValue("@model", model);
                insertCmd.Parameters.AddWithValue("@battery", battery);
                insertCmd.Parameters.AddWithValue("@isAvail", isAvail);
                insertCmd.Parameters.AddWithValue("@price", price);
                insertCmd.Parameters.AddWithValue("@location", location);
                insertCmd.Parameters.AddWithValue("@zonaId", GetZona(zonaNaziv));
                insertCmd.Parameters.AddWithValue("@status", status);
                insertCmd.ExecuteNonQuery();
            }
        }
    }
}
