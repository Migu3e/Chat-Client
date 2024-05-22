using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Client.Firestore
{
    public static class FirestoreHelper
    {
        static string fireconfig = @"
        {
            ""type"": ""service_account"",
            ""project_id"": ""chat-proj-108d6"",
            ""private_key_id"": ""021519c8b96201fa0f438902002bcdbce35235b6"",
            ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvwIBADANBgkqhkiG9w0BAQEFAASCBKkwggSlAgEAAoIBAQDD1+UfqppHA5nR\n98aRg9uFbJEN7rAKzOjlgJelKBXgKB/3Ncy0JTIdE1XKir5MczYMS7CUWqSZ2Lw1\nSLdLGkCQjiw0P9hFZJ52/DmllKQ4gAXgjSQuaT7vChA27LpDd2M2x/lv7KFybhJU\nOOuiWk9aUPL3qBSIZsTrHSoU1FGlVd7AifuOZ/mDTYX7uPiZAzJUuTVVVZNBMC4S\nsdbIzkSn7A1SPplWCjbEfISkJ42d/eMTmfLgxRYX1+dpEMjtvYDBtq0NE+XQbR/l\nvQIdHpKXvuOkHeBNxzoIH/XUVjWwhNiRx4KLvePUQrDRPAwy4oL8WHCs34GPum1y\n3MWdkpQHAgMBAAECggEAHA6ZD7FUSuo0JkWWGBDI6VOFcHaKl/GFSCPlFc8pWyTE\nd5koi4JsrIexmcPIwZEb5CUP1iEqRbQsOoIBaGwJZexIXu/qnv5sSWWXKKaj5EhL\nD0GJNnDziDFr40KE6Nd4ykdZ7/P5qTglvhUthOPlTG4ecOaOkoAOpBRYjAVi8Qmc\nx5iZJe7t2UmsPFynm6Cs6C/ZchPrtESAZKk1BQqI4Q29zHg+X1/vN1G8QT3VyZ2J\nNSquzZB9LAjFpTIJdg/tnG3bgp45wXFNY6glyERKSp42vjMhWf24PLo5zJq68tE4\nhN/X/QPKhJgWcymxn4qYr0xAAuVsnx9owvyXIAWwCQKBgQD5olX7eOYYtzliEyxB\nUpTQpE3FWfJpzVE/f2vR2U1WCHkEJ13ep+As9eSNKVJAxRrz2XsXUU5Ok7kQV9Bk\nrKY9+7nAYnXTAA2fgLjjQVeQGegFEjqfDD2dL1iWyPCPadrg1YohHO7qyKBXUfuP\ncgXMM6TNoyOH5D//tmzGg+zgOwKBgQDI1mbNw4s1ga7VPsSAvEtNkhROM5KOW9gK\n+2LELBLOn7SBYJcyf1Ac2+aWCC1sRccgxwG2EB/A7kSxqM3pSDlTLYEHl57Mqu9x\nbTxbtG4+bLPZn5sfFqVx6wHQqpaomyEVXu0BGymeGl5Y3PYHo8Nuqx6zqpBx193J\n90uJ8u9KpQKBgQCA6O7D3Kvd6YgteD8rQGzuzVoH5qoDLu6c/rz78d445kkv9vBH\nkaN3NEehCcya++4uLImfAfKRK0GvCdnokyuJ6JsEmRPFMUrXqk5PtKd2F5q0O7Sf\n+18584Ao5X0sYfqQjlU1Qk6yWYZLcV+ZtMT/rU+WjX9Epn7SGy9S2D+TjwKBgQCa\nRP6gzbTP+/Z0/QrqLDjXs/7+9uR7j5cLEICUWZp0tv5rZuudMWgzUsLzugJSKeNE\njTkGRapug1em9Bh/Oig2eTykvVWQtzjH8vWrJ5SLROp9nvkDz1x0feVeCLtDdi3a\ngZkiAWBdfrm8HraptaR4DN+/eKQNnphR/DA9HaurtQKBgQDKHebCoejoHvSZyfT1\n0ab/cNi/8f7L9/kao6QokVopmsksU1hLp/+UtuNhhC7EXHqwCz73OWDNzhUnx74D\n1Tnvofhqm71krydcBOG+e01u4jyypQqIg6W6nF+Z14LIUrKVVKrs/hkgDmg/a8E6\nBDM4fzWQYb+2+fSyvorePzr9BQ==\n-----END PRIVATE KEY-----\n"",
            ""client_email"": ""firebase-adminsdk-o6j18@chat-proj-108d6.iam.gserviceaccount.com"",
            ""client_id"": ""116880827885422652839"",
            ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
            ""token_uri"": ""https://oauth2.googleapis.com/token"",
            ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
            ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-o6j18%40chat-proj-108d6.iam.gserviceaccount.com"",
        }";

        static string filePath = "";
        public static FirestoreDb? database { get; private set; }
        public static void SetEnviornmentVariable()
        {
            filePath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetRandomFileName()));
            File.WriteAllText(filePath, fireconfig);
            File.SetAttributes(filePath, FileAttributes.Hidden);
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filePath);
            database = FirestoreDb.Create("chat-proj-108d6");
            File.Delete(filePath);
        }
    }
}
