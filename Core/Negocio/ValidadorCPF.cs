using Core.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Dominio.Cliente;
using Core.DAO;

namespace Core.Negocio
{
    class ValidadorCPF : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            StringBuilder sb = new StringBuilder();
            if (entidade.GetType() == typeof(ClientePF))
            {
                string cpf = ((ClientePF)entidade).CPF;
                String cpfGerado = "";                          // para efetuar o cálculo

                if (String.IsNullOrEmpty(cpf))
                {
                    return "\nDIGITE O CPF!";
                }

                // remove os caracteres indesejados
                cpf = removerCaracteres(cpf);


                if (!verificaTamanho(cpf))
                { // verifica se tamanho do cpf é válido
                    return "\nO CPF DEVE TER 11 DIGITOS!";
                }

                if (verificaSeDigIguais(cpf))
                { // verifica se dígitos iguais
                    return "\nO CPF NÃO DEVE SER DE DÍGITOS IGUAIS!";
                }

                cpfGerado = cpf.Substring(0, 9);
                cpfGerado += calculoComCpf(cpfGerado);      // para calculo do primeiro dígito verificador
                cpfGerado += calculoComCpf(cpfGerado);      // para calculo do segundo dígito verificador

                if (!cpfGerado.Equals(cpf))
                { // verifica se são iguais
                    return "\nCPF INFORMADO INCORRETO!";
                }

            }
            else
            {
                sb.Append("CPF NÃO PODE SER VALIDADO, POIS ENTIDADE NÃO É CLIENTE PF! <br />");
            }

            if (sb.Length != 0)
            {
                return sb.ToString();
            }

            return null;
        }

        // Método para remoção de caracteres desnecessários
        private String removerCaracteres(String cpf)
        {
            cpf = cpf.Replace("-", "");             // substitui o caracter "-" por ""
            cpf = cpf.Replace(".", "");             // substitui o caracter "." por ""
            return cpf;
        }

        // Método para verificar se o tamanho do cpf é de 11 dígitos
        private bool verificaTamanho(String cpf)
        {
            if (cpf.Length != 11)
                return false;
            return true;
        }

        // Método para verificação se os dígitos são iguais
        private bool verificaSeDigIguais(String cpf)
        {
            char primDig = cpf.First();                 // para considerar qualquer sequência de dígitos iguais inválidas
            //char primDig = '0';                       // para considerar apenas sequência do dígito '0' inválida
            char[] charCpf = cpf.ToCharArray();         // transforma em array de char
            foreach (var c in charCpf)                  // percorre o array
            {
                if (c != primDig)                       // verifica se o dígito verificado é diferente do primeiro dígito
                {
                    // se tiver um dígito diferente já sai do loop e retorna false
                    // indicando que o CPF NÃO É DE DÍGITOS IGUAIS
                    return false;
                }
            }

            // retorna true indicando CPF DE DÍTIGOS IGUAIS
            return true;
        }

        // Método para cálculo do dígito do CPF
        private String calculoComCpf(String cpf)
        {
            int digGerado = 0;                                  // inicializa a variável do dígito gerado
            int mult = cpf.Length + 1;                          // multiplicador
            char[] charCpf = cpf.ToCharArray();                 // transforma em array de char
            
            // percorre o cpf
            for (int i = 0; i < cpf.Length; i++)
                digGerado += (charCpf[i] - 48) * mult--;        // calcula o o dígito
            if (digGerado % 11 < 2)                             // verifica se módulo de 11 é menor que 2
                digGerado = 0;                                  // atribui 0 para o dígito gerado
            else
                digGerado = 11 - digGerado % 11;                // deduz o valor do módulo de 11 do valor de 11 e atribui ao dígito gerado
            return digGerado.ToString();                        // retorna dígito gerado transformado em string
        }

    }
}
