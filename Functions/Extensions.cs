using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Functions
{
    public static class Extensions
    {
        public static int ParseToInt(this object p) { try { return p is DBNull || (p?.Equals(string.Empty) ?? true) ? 0 : Convert.ToInt32(p); } catch (Exception) { return 0; } }
        public static double ParseToDouble(this object p) { try { return p is DBNull || (p?.Equals(string.Empty) ?? true) ? 0 : Convert.ToDouble(p); } catch (Exception) { return 0; } }
        public static string validarCpf(this string o) { if (!o.ToString().All(char.IsNumber) || o.ToString().Length != 11) return "Cpf inválido\n"; else { return null; } }
        public static string validarNome(this string o) { if (!o.ToString().All(char.IsLetter) || o.ToString().Length > 100) return "Nome inválido\n"; else { return null; } }
        public static string validarCep(this string o) { if (!o.ToString().All(char.IsNumber) || o.ToString().Length != 8) return "Cep inválido\n"; else { return null; } }
        public static string validarNrRes(this int o) { if (o.ToString().Length > 6 || o < 0) return "Número de residência inválido\n"; else { return null; } }

    }
}
