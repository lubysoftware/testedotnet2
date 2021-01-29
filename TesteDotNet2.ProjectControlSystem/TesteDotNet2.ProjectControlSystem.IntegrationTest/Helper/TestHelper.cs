using System;
using System.Collections.Generic;
using System.Text;

namespace TesteDotNet2.ProjectControlSystem.IntegrationTest.Helper
{
    public static class TestHelper
    {
        public static string GetAleatoryName()
        {
            string letras = "ABCDEFGHIJKLMNOPQRSTUVYWXZ";

            Random random = new Random();

            string name = "";
            int index = -1;
            for (int i = 1; i < 7; i++)
            {
                index = random.Next(letras.Length);
                name += letras.Substring(index, 1);
            }
            return name;
        }

        public static string GetAleatoryCPF()
        {
            string letras = "0123456789";

            Random random = new Random();

            string cpf = "";
            int index = -1;
            for (int i = 1; i < 12; i++)
            {
                index = random.Next(letras.Length);
                cpf += letras.Substring(index, 1);
            }
            return cpf;
        }
    }
}
