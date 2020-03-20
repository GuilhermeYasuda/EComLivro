using Core.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacao;
using Dominio;
using Core.DAO;
using Core.Negocio;
using Dominio.Cliente;

namespace Core.Controle
{
    public sealed class Fachada : IFachada
    {
        /*
         * Mapa que conterá o nome das Classes que estão vindo da view 
         * e vinculará com a DAOs específica da classe
         */
        private Dictionary<string, IDAO> daos;

        /*
         * private Dictionary< (1)string, (2)(Dictionary<(3)string, (4)List<IStrategy>>) >
         * 1 - string que conterá operação que vai ser executada: SALVAR, ALTERAR, CONSULTAR ou EXCLUIR
         * 2 - Mapa que conterá a vinculação entre os nomes das classes e as Strategys(lista de regras) referente a classe
         * 3 - string que conterá o nome da classe que funcionará como indice do Mapa
         * 4 - lista de Strategys que conterá todas as regras que deverão ser executadas para a conclusão da operação que vai ser executada (1)
         */
        private Dictionary<string, Dictionary<string, List<IStrategy>>> rns;
        private Resultado resultado;
        private static readonly Fachada Instance = new Fachada();

        /*
         * INÍCIO do CONSTRUTOR da Fachada 
         * ------------------------------------------------------
         */
        private Fachada()
        {
            daos = new Dictionary<string, IDAO>();
            rns = new Dictionary<string, Dictionary<string, List<IStrategy>>>();

            // instâncias das Strategys
            ComplementoDtCadastro complementoDtCadastro = new ComplementoDtCadastro();
            DeleteCartao deleteCartao = new DeleteCartao();
            DeleteEndereco deleteEndereco = new DeleteEndereco();
            DeleteClienteXCartoes deleteClienteXCartoes = new DeleteClienteXCartoes();
            DeleteClienteXEnderecos deleteClienteXEnderecos = new DeleteClienteXEnderecos();
            ValidadorClienteCC validadorClienteCC = new ValidadorClienteCC();
            ValidadorClienteEndereco validadorClienteEndereco = new ValidadorClienteEndereco();
            ValidadorCartaoCredito validadorCartaoCredito = new ValidadorCartaoCredito();
            ValidadorDadosClientePessoaFisica valDadosClientePessoaFisica = new ValidadorDadosClientePessoaFisica();
            ValidadorEndereco valEndereco = new ValidadorEndereco();
            ParametroExcluir paramExcluir = new ParametroExcluir();

            // instâncias das DAOs
            EnderecoDAO enderecoDAO = new EnderecoDAO();
            CidadeDAO cidadeDAO = new CidadeDAO();
            EstadoDAO estadoDAO = new EstadoDAO();
            PaisDAO paisDAO = new PaisDAO();
            ClientePFXCartaoDAO clientePFXCartaoDAO = new ClientePFXCartaoDAO();
            ClientePFXEnderecoDAO clientePFXEnderecoDAO = new ClientePFXEnderecoDAO();
            CartaoCreditoDAO ccDAO = new CartaoCreditoDAO();
            BandeiraDAO bandeiraDAO = new BandeiraDAO();
            TipoTelefoneDAO tipoTelefoneDAO = new TipoTelefoneDAO();
            TipoResidenciaDAO tipoResidenciaDAO = new TipoResidenciaDAO();
            TipoLogradouroDAO tipoLogradouroDAO = new TipoLogradouroDAO();
            ClientePFDAO clientePFDAO = new ClientePFDAO();

            // adicionando as DAOs ao Mapa daos já indicando o indice (nome da classe domínio) de cada um
            daos.Add(typeof(Endereco).Name, enderecoDAO);
            daos.Add(typeof(Cidade).Name, cidadeDAO);
            daos.Add(typeof(Estado).Name, estadoDAO);
            daos.Add(typeof(Pais).Name, paisDAO);
            daos.Add(typeof(ClientePFXCC).Name, clientePFXCartaoDAO);
            daos.Add(typeof(ClientePFXEndereco).Name, clientePFXEnderecoDAO);
            daos.Add(typeof(CartaoCredito).Name, ccDAO);
            daos.Add(typeof(Bandeira).Name, bandeiraDAO);
            daos.Add(typeof(TipoTelefone).Name, tipoTelefoneDAO);
            daos.Add(typeof(TipoResidencia).Name, tipoResidenciaDAO);
            daos.Add(typeof(TipoLogradouro).Name, tipoLogradouroDAO);
            daos.Add(typeof(ClientePF).Name, clientePFDAO);

            /*
             * CLIENTE X ENDEREÇO - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            List<IStrategy> rnsSalvarClienteEndereco = new List<IStrategy>();
            rnsSalvarClienteEndereco.Add(validadorClienteEndereco);
            //List<IStrategy> rnsAlterarClienteEndereco = new List<IStrategy>();
            //rnsAlterarClienteEndereco.Add(validadorClienteEndereco);
            //List<IStrategy> rnsExcluirClienteEndereco = new List<IStrategy>();
            //rnsExcluirClienteCartao.Add(paramExcluir);
            //List<IStrategy> rnsConsultarClienteEndereco = new List<IStrategy>();
            /*
             * CLIENTE X ENDEREÇO - FIM ---------------------------------------------------------------------
             */

            // criando as listas que conterão as Strategys referente a cada classe
            // e adicionando as strategy nas listas
            /*
             * ENDEREÇO - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            //List<IStrategy> rnsSalvarEndereco = new List<IStrategy>();
            //rnsSalvarEndereco.Add(valEndereco);
            List<IStrategy> rnsAlterarEndereco = new List<IStrategy>();
            rnsAlterarEndereco.Add(valEndereco);
            List<IStrategy> rnsExcluirEndereco = new List<IStrategy>();
            rnsExcluirEndereco.Add(deleteEndereco);
            rnsExcluirEndereco.Add(paramExcluir);
            List<IStrategy> rnsConsultarEndereco = new List<IStrategy>();
            /*
             * ENDEREÇO - FIM ---------------------------------------------------------------------
             */

            /*
             * CIDADE - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            //List<IStrategy> rnsSalvarCidade = new List<IStrategy>();
            //List<IStrategy> rnsAlterarCidade = new List<IStrategy>();
            //List<IStrategy> rnsExcluirCidade = new List<IStrategy>();
            List<IStrategy> rnsConsultarCidade = new List<IStrategy>();
            /*
             * CIDADE - FIM ---------------------------------------------------------------------
             */

            /*
             * ESTADO - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            //List<IStrategy> rnsSalvarEstado = new List<IStrategy>();
            //List<IStrategy> rnsAlterarEstado = new List<IStrategy>();
            //List<IStrategy> rnsExcluirEstado = new List<IStrategy>();
            List<IStrategy> rnsConsultarEstado = new List<IStrategy>();
            /*
             * ESTADO - FIM ---------------------------------------------------------------------
             */

            /*
             * PAIS - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            //List<IStrategy> rnsSalvarPais = new List<IStrategy>();
            //List<IStrategy> rnsAlterarPais = new List<IStrategy>();
            //List<IStrategy> rnsExcluirPais = new List<IStrategy>();
            List<IStrategy> rnsConsultarPais = new List<IStrategy>();
            /*
             * PAIS - FIM ---------------------------------------------------------------------
             */

            /*
             * ClientePF - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            List<IStrategy> rnsSalvarClientePF = new List<IStrategy>();
            rnsSalvarClientePF.Add(valDadosClientePessoaFisica);
            rnsSalvarClientePF.Add(complementoDtCadastro);
            List<IStrategy> rnsAlterarClientePF = new List<IStrategy>();
            rnsAlterarClientePF.Add(valDadosClientePessoaFisica);
            List<IStrategy> rnsExcluirClientePF = new List<IStrategy>();
            rnsExcluirClientePF.Add(deleteClienteXCartoes);
            rnsExcluirClientePF.Add(deleteClienteXEnderecos);
            rnsExcluirClientePF.Add(paramExcluir);
            List<IStrategy> rnsConsultarClientePF = new List<IStrategy>();
            /*
             * ClientePF - FIM ---------------------------------------------------------------------
             */

            /*
             * CLIENTE X CARTÃO - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            List<IStrategy> rnsSalvarClienteCartao = new List<IStrategy>();
            rnsSalvarClienteCartao.Add(validadorClienteCC);
            //List<IStrategy> rnsAlterarClienteCartao = new List<IStrategy>();
            //rnsAlterarClienteCartao.Add(validadorClienteCC);
            //List<IStrategy> rnsExcluirClienteCartao = new List<IStrategy>();
            //rnsExcluirClienteCartao.Add(paramExcluir);
            //List<IStrategy> rnsConsultarClienteCartao = new List<IStrategy>();
            /*
             * CLIENTE X CARTÃO - FIM ---------------------------------------------------------------------
             */

            /*
             * CARTÃO - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            //List<IStrategy> rnsSalvarCartao = new List<IStrategy>();
            //rnsSalvarCartao.Add(validadorCartaoCredito);
            List<IStrategy> rnsAlterarCartao = new List<IStrategy>();
            rnsAlterarCartao.Add(validadorCartaoCredito);
            List<IStrategy> rnsExcluirCartao = new List<IStrategy>();
            rnsExcluirCartao.Add(deleteCartao);
            rnsExcluirCartao.Add(paramExcluir);
            List<IStrategy> rnsConsultarCartao = new List<IStrategy>();
            /*
             * CARTÃO - FIM ---------------------------------------------------------------------
             */

            /*
             * Bandeira - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            //List<IStrategy> rnsSalvarBandeira = new List<IStrategy>();
            //List<IStrategy> rnsAlterarBandeira = new List<IStrategy>();
            //List<IStrategy> rnsExcluirBandeira = new List<IStrategy>();
            List<IStrategy> rnsConsultarBandeira = new List<IStrategy>();
            /*
             * Bandeira - FIM ---------------------------------------------------------------------
             */

            /*
             * Telefone - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            //List<IStrategy> rnsSalvarTelefone = new List<IStrategy>();
            //List<IStrategy> rnsAlterarTelefone = new List<IStrategy>();
            //List<IStrategy> rnsExcluirTelefone = new List<IStrategy>();
            //List<IStrategy> rnsConsultarTelefone = new List<IStrategy>();
            /*
             * Telefone - FIM ---------------------------------------------------------------------
             */

            /*
             * TipoTelefone - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            //List<IStrategy> rnsSalvarTipoTelefone = new List<IStrategy>();
            //List<IStrategy> rnsAlterarTipoTelefone = new List<IStrategy>();
            //List<IStrategy> rnsExcluirTipoTelefone = new List<IStrategy>();
            List<IStrategy> rnsConsultarTipoTelefone = new List<IStrategy>();
            /*
             * TipoTelefone - FIM ---------------------------------------------------------------------
             */

            /*
             * TipoResidencia - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            //List<IStrategy> rnsSalvarTipoResidencia = new List<IStrategy>();
            //List<IStrategy> rnsAlterarTipoResidencia = new List<IStrategy>();
            //List<IStrategy> rnsExcluirTipoResidencia = new List<IStrategy>();
            List<IStrategy> rnsConsultarTipoResidencia = new List<IStrategy>();
            /*
             * TipoResidencia - FIM ---------------------------------------------------------------------
             */

            /*
             * TipoLogradouro - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            //List<IStrategy> rnsSalvarTipoLogradouro = new List<IStrategy>();
            //List<IStrategy> rnsAlterarTipoLogradouro = new List<IStrategy>();
            //List<IStrategy> rnsExcluirTipoLogradouro = new List<IStrategy>();
            List<IStrategy> rnsConsultarTipoLogradouro = new List<IStrategy>();
            /*
             * TipoLogradouro - FIM ---------------------------------------------------------------------
             */

            // criando mapa indicando o indice (operação) e a lista das Stategys(regras) de cada operação
            /*
             * CIDADE - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsCidade = new Dictionary<string, List<IStrategy>>();
            //rnsCidade.Add("SALVAR", rnsSalvarCidade);
            //rnsCidade.Add("ALTERAR", rnsAlterarCidade);
            //rnsCidade.Add("EXCLUIR", rnsExcluirCidade);
            rnsCidade.Add("CONSULTAR", rnsConsultarCidade);
            /*
             * CIDADE - FIM ----------------------------------------------------------------------------
             */

            /*
             * ESTADO - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsEstado = new Dictionary<string, List<IStrategy>>();
            //rnsEstado.Add("SALVAR", rnsSalvarEstado);
            //rnsEstado.Add("ALTERAR", rnsAlterarEstado);
            //rnsEstado.Add("EXCLUIR", rnsExcluirEstado);
            rnsEstado.Add("CONSULTAR", rnsConsultarEstado);
            /*
             * ESTADO - FIM ----------------------------------------------------------------------------
             */

            /*
             * PAIS - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsPais = new Dictionary<string, List<IStrategy>>();
            //rnsPais.Add("SALVAR", rnsSalvarPais);
            //rnsPais.Add("ALTERAR", rnsAlterarPais);
            //rnsPais.Add("EXCLUIR", rnsExcluirPais);
            rnsPais.Add("CONSULTAR", rnsConsultarPais);
            /*
             * PAIS - FIM ----------------------------------------------------------------------------
             */

            /*
             * CLIENTE X ENDEREÇO - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsClienteEndereco = new Dictionary<string, List<IStrategy>>();
            rnsClienteEndereco.Add("SALVAR", rnsSalvarClienteEndereco);
            //rnsClienteEndereco.Add("ALTERAR", rnsAlterarClienteEndereco);
            //rnsClienteEndereco.Add("EXCLUIR", rnsExcluirClienteEndereco);
            //rnsClienteEndereco.Add("CONSULTAR", rnsConsultarClienteEndereco);
            /*
             * CLIENTE X ENDEREÇO - FIM ----------------------------------------------------------------------------
             */

            /*
             * ENDEREÇO - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsEndereco = new Dictionary<string, List<IStrategy>>();
            //rnsEndereco.Add("SALVAR", rnsSalvarEndereco);
            rnsEndereco.Add("ALTERAR", rnsAlterarEndereco);
            rnsEndereco.Add("EXCLUIR", rnsExcluirEndereco);
            rnsEndereco.Add("CONSULTAR", rnsConsultarEndereco);
            /*
             * ENDEREÇO - FIM ----------------------------------------------------------------------------
             */

            /*
             * ClientePF - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsClientePF = new Dictionary<string, List<IStrategy>>();
            rnsClientePF.Add("SALVAR", rnsSalvarClientePF);
            rnsClientePF.Add("ALTERAR", rnsAlterarClientePF);
            rnsClientePF.Add("EXCLUIR", rnsExcluirClientePF);
            rnsClientePF.Add("CONSULTAR", rnsConsultarClientePF);
            /*
             * ClientePF - FIM ----------------------------------------------------------------------------
             */

            /*
             * CLIENTE X CARTÃO - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsClienteCartao = new Dictionary<string, List<IStrategy>>();
            rnsClienteCartao.Add("SALVAR", rnsSalvarClienteCartao);
            //rnsClienteCartao.Add("ALTERAR", rnsAlterarClienteCartao);
            //rnsClienteCartao.Add("EXCLUIR", rnsExcluirClienteCartao);
            //rnsClienteCartao.Add("CONSULTAR", rnsConsultarClienteCartao);
            /*
             * CLIENTE X CARTÃO - FIM ----------------------------------------------------------------------------
             */

            /*
             * CARTÃO - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsCartao = new Dictionary<string, List<IStrategy>>();
            //rnsCartao.Add("SALVAR", rnsSalvarCartao);
            rnsCartao.Add("ALTERAR", rnsAlterarCartao);
            rnsCartao.Add("EXCLUIR", rnsExcluirCartao);
            rnsCartao.Add("CONSULTAR", rnsConsultarCartao);
            /*
             * CARTÃO - FIM ----------------------------------------------------------------------------
             */

            /*
             * Bandeira - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsBandeira = new Dictionary<string, List<IStrategy>>();
            //rnsBandeira.Add("SALVAR", rnsSalvarBandeira);
            //rnsBandeira.Add("ALTERAR", rnsAlterarBandeira);
            //rnsBandeira.Add("EXCLUIR", rnsExcluirBandeira);
            rnsBandeira.Add("CONSULTAR", rnsConsultarBandeira);
            /*
             * Bandeira - FIM ----------------------------------------------------------------------------
             */

            /*
             * TELEFONE - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            //Dictionary<string, List<IStrategy>> rnsTelefone = new Dictionary<string, List<IStrategy>>();
            //rnsTelefone.Add("SALVAR", rnsSalvarTelefone);
            //rnsTelefone.Add("ALTERAR", rnsAlterarTelefone);
            //rnsTelefone.Add("EXCLUIR", rnsExcluirTelefone);
            //rnsTelefone.Add("CONSULTAR", rnsConsultarTelefone);
            /*
             * TELEFONE - FIM ----------------------------------------------------------------------------
             */

            /*
             * TipoTelefone - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsTipoTelefone = new Dictionary<string, List<IStrategy>>();
            //rnsTipoTelefone.Add("SALVAR", rnsSalvarTipoTelefone);
            //rnsTipoTelefone.Add("ALTERAR", rnsAlterarTipoTelefone);
            //rnsTipoTelefone.Add("EXCLUIR", rnsExcluirTipoTelefone);
            rnsTipoTelefone.Add("CONSULTAR", rnsConsultarTipoTelefone);
            /*
             * TipoTelefone - FIM ----------------------------------------------------------------------------
             */

            /*
             * TipoResidencia - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsTipoResidencia = new Dictionary<string, List<IStrategy>>();
            //rnsTipoResidencia.Add("SALVAR", rnsSalvarTipoResidencia);
            //rnsTipoResidencia.Add("ALTERAR", rnsAlterarTipoResidencia);
            //rnsTipoResidencia.Add("EXCLUIR", rnsExcluirTipoResidencia);
            rnsTipoResidencia.Add("CONSULTAR", rnsConsultarTipoResidencia);
            /*
             * TipoResidencia - FIM ----------------------------------------------------------------------------
             */

            /*
             * TipoLogradouro - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsTipoLogradouro = new Dictionary<string, List<IStrategy>>();
            //rnsTipoLogradouro.Add("SALVAR", rnsSalvarTipoLogradouro);
            //rnsTipoLogradouro.Add("ALTERAR", rnsAlterarTipoLogradouro);
            //rnsTipoLogradouro.Add("EXCLUIR", rnsExcluirTipoLogradouro);
            rnsTipoLogradouro.Add("CONSULTAR", rnsConsultarTipoLogradouro);
            /*
             * TipoLogradouro - FIM ----------------------------------------------------------------------------
             */


            // adicionando ao mapa geral que conterá todos os mapas
            rns.Add(typeof(ClientePFXEndereco).Name, rnsClienteEndereco);
            rns.Add(typeof(Endereco).Name, rnsEndereco);
            rns.Add(typeof(Cidade).Name, rnsCidade);
            rns.Add(typeof(Estado).Name, rnsEstado);
            rns.Add(typeof(Pais).Name, rnsPais);
            rns.Add(typeof(ClientePFXCC).Name, rnsClienteCartao);
            rns.Add(typeof(CartaoCredito).Name, rnsCartao);
            rns.Add(typeof(Bandeira).Name, rnsBandeira);
            rns.Add(typeof(TipoTelefone).Name, rnsTipoTelefone);
            rns.Add(typeof(TipoResidencia).Name, rnsTipoResidencia);
            rns.Add(typeof(TipoLogradouro).Name, rnsTipoLogradouro);
            rns.Add(typeof(ClientePF).Name, rnsClientePF);

        }
        // FIM do CONSTRUTOR da Fachada -------------------------

        public static Fachada UniqueInstance
        {
            get { return Instance; }
        }

        public Resultado salvar(EntidadeDominio entidade)
        {
            resultado = new Resultado();
            string nmClasse = entidade.GetType().Name;
            string msg = executarRegras(entidade, "SALVAR");

            if (string.IsNullOrEmpty(msg))
            {
                IDAO dao = daos[nmClasse];
                dao.Salvar(entidade);
                List<EntidadeDominio> entidades = new List<EntidadeDominio>();
                entidades.Add(entidade);
                resultado.Entidades = entidades;
            }
            else
            {
                resultado.Msg = msg;
            }
            return resultado;
        }

        public Resultado alterar(EntidadeDominio entidade)
        {
            resultado = new Resultado();
            string nmClasse = entidade.GetType().Name;
            string msg = executarRegras(entidade, "ALTERAR");

            if (string.IsNullOrEmpty(msg))
            {
                IDAO dao = daos[nmClasse];
                dao.Alterar(entidade);
                List<EntidadeDominio> entidades = new List<EntidadeDominio>();
                entidades.Add(entidade);
                resultado.Entidades = entidades;
            }
            else
            {
                resultado.Msg = msg;
            }

            return resultado;
        }

        public Resultado consultar(EntidadeDominio entidade)
        {
            resultado = new Resultado();
            string nmClasse = entidade.GetType().Name;
            string msg = executarRegras(entidade, "CONSULTAR");

            if (string.IsNullOrEmpty(msg))
            {
                IDAO dao = daos[nmClasse];
                try
                {
                    resultado.Entidades = dao.Consultar(entidade);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {
                resultado.Msg = msg;
            }
            return resultado;
        }

        public Resultado excluir(EntidadeDominio entidade)
        {
            resultado = new Resultado();
            string nmClasse = entidade.GetType().Name;
            string msg = executarRegras(entidade, "EXCLUIR");

            if (string.IsNullOrEmpty(msg))
            {
                IDAO dao = daos[nmClasse];
                dao.Excluir(entidade);
                List<EntidadeDominio> entidades = new List<EntidadeDominio>();
                entidades.Add(entidade);
                resultado.Entidades = entidades;
            }
            else
            {
                resultado.Msg = msg;
            }

            return resultado;
        }

        /*
         * Método que percorrerá a lista que contém as regra que devem ser 
         * executadas para validações e verificações "obrigatórias"
         */
        private string executarRegras(EntidadeDominio entidade, string operacao)
        {
            // pega nome da classe
            string nmClasse = entidade.GetType().Name;
            StringBuilder msg = new StringBuilder();

            // pegando o mapa específico que contém todas as listas das regras indicando o indice (nmClasse)
            Dictionary<string, List<IStrategy>> regrasOperacao = rns[nmClasse];

            if (regrasOperacao != null)
            {
                // pegando a lista específica indicando a operação que será feita
                List<IStrategy> regras = regrasOperacao[operacao];

                if (regras != null)
                {
                    // percorre a lista para execução das Strategys
                    foreach (IStrategy s in regras)
                    {
                        // chama método que fará a validação/verificação
                        string m = s.processar(entidade);

                        if (!string.IsNullOrEmpty(m))
                        {
                            msg.Append(m);
                            msg.Append("\n");
                        }
                    }
                }
            }

            if (msg.Length > 0)
                return msg.ToString();
            else
                return null;
        }
    }
}
