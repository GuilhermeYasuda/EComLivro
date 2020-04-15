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
using Dominio.Livro;
using Dominio.Venda;

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
            ValidadorAtivacaoInativacaoLivro valAtivacaoInativacaoLivro = new ValidadorAtivacaoInativacaoLivro();
            ValidadorDadosEstoque valDadosEstoque = new ValidadorDadosEstoque();
            ValidadorDadosPedido valDadosPedido = new ValidadorDadosPedido();
            ValidadorStatusPedido valStatusPedido = new ValidadorStatusPedido();
            ValidadorAtualizaPedido valAtualizaPedido = new ValidadorAtualizaPedido();
            ValidadorExistenciaCPF valExistenciaCPF = new ValidadorExistenciaCPF();
            ValidadorExistenciaEmail valExistenciaEmail = new ValidadorExistenciaEmail();

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
            CategoriaMotivoDAO categoriaMotivoDAO = new CategoriaMotivoDAO();
            CategoriaLivroDAO categoriaLivroDAO = new CategoriaLivroDAO();
            EditoraDAO editoraDAO = new EditoraDAO();
            LivroDAO livroDAO = new LivroDAO();
            EstoqueDAO estoqueDAO = new EstoqueDAO();
            FornecedorDAO fornecedorDAO = new FornecedorDAO();
            CupomDAO cupomDAO = new CupomDAO();
            TipoCupomDAO tipoCupomDAO = new TipoCupomDAO();
            PedidoXCupomDAO clientePFXCupomDAO = new PedidoXCupomDAO();
            StatusPedidoDAO statusPedidoDAO = new StatusPedidoDAO();
            PedidoDetalheDAO pedidoDetalheDAO = new PedidoDetalheDAO();
            CCPedidoDAO ccPedidoDAO = new CCPedidoDAO();
            PedidoDAO pedidoDAO = new PedidoDAO();

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
            daos.Add(typeof(CategoriaMotivo).Name, categoriaMotivoDAO);
            daos.Add(typeof(Categoria).Name, categoriaLivroDAO);
            daos.Add(typeof(Editora).Name, editoraDAO);
            daos.Add(typeof(Livro).Name, livroDAO);
            daos.Add(typeof(Estoque).Name, estoqueDAO);
            daos.Add(typeof(Fornecedor).Name, fornecedorDAO);
            daos.Add(typeof(Cupom).Name, cupomDAO);
            daos.Add(typeof(TipoCupom).Name, tipoCupomDAO);
            daos.Add(typeof(PedidoXCupom).Name, clientePFXCupomDAO);
            daos.Add(typeof(StatusPedido).Name, statusPedidoDAO);
            daos.Add(typeof(PedidoDetalhe).Name, pedidoDetalheDAO);
            daos.Add(typeof(CartaoCreditoPedido).Name, ccPedidoDAO);
            daos.Add(typeof(Pedido).Name, pedidoDAO);

            #region CRIAÇÃO DA LISTA DE STRATEGYS

            /*
             * CLIENTE X ENDEREÇO - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            List<IStrategy> rnsSalvarClienteEndereco = new List<IStrategy>();
            rnsSalvarClienteEndereco.Add(validadorClienteEndereco);
            //List<IStrategy> rnsAlterarClienteEndereco = new List<IStrategy>();
            //rnsAlterarClienteEndereco.Add(validadorClienteEndereco);
            //List<IStrategy> rnsExcluirClienteEndereco = new List<IStrategy>();
            //rnsExcluirClienteCartao.Add(paramExcluir);
            List<IStrategy> rnsConsultarClienteEndereco = new List<IStrategy>();
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
            rnsSalvarClientePF.Add(valExistenciaCPF);
            rnsSalvarClientePF.Add(valExistenciaEmail);
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
            List<IStrategy> rnsConsultarClienteCartao = new List<IStrategy>();
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

            /*
             * CategoriaMotivo - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            //List<IStrategy> rnsSalvarCategoriaMotivo = new List<IStrategy>();
            //List<IStrategy> rnsAlterarCategoriaMotivo = new List<IStrategy>();
            //List<IStrategy> rnsExcluirCategoriaMotivo = new List<IStrategy>();
            List<IStrategy> rnsConsultarCategoriaMotivo = new List<IStrategy>();
            /*
             * CategoriaMotivo - FIM ---------------------------------------------------------------------
             */

            /*
             * CategoriaLivro - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            //List<IStrategy> rnsSalvarCategoriaLivro = new List<IStrategy>();
            //List<IStrategy> rnsAlterarCategoriaLivro = new List<IStrategy>();
            //List<IStrategy> rnsExcluirCategoriaLivro = new List<IStrategy>();
            List<IStrategy> rnsConsultarCategoriaLivro = new List<IStrategy>();
            /*
             * CategoriaLivro - FIM ---------------------------------------------------------------------
             */

            /*
             * EDITORA - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            //List<IStrategy> rnsSalvarEditora = new List<IStrategy>();
            //List<IStrategy> rnsAlterarEditora = new List<IStrategy>();
            //List<IStrategy> rnsExcluirEditora = new List<IStrategy>();
            List<IStrategy> rnsConsultarEditora = new List<IStrategy>();
            /*
             * EDITORA - FIM ---------------------------------------------------------------------
             */

            /*
             * LIVRO - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            //List<IStrategy> rnsSalvarLivro = new List<IStrategy>();
            List<IStrategy> rnsAlterarLivro = new List<IStrategy>();
            rnsAlterarLivro.Add(valAtivacaoInativacaoLivro);
            //List<IStrategy> rnsExcluirLivro = new List<IStrategy>();
            List<IStrategy> rnsConsultarLivro = new List<IStrategy>();
            /*
             * LIVRO - FIM ---------------------------------------------------------------------
             */

            /*
             * ESTOQUE - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            List<IStrategy> rnsSalvarEstoque = new List<IStrategy>();
            rnsSalvarEstoque.Add(valDadosEstoque);
            rnsSalvarEstoque.Add(complementoDtCadastro);
            List<IStrategy> rnsAlterarEstoque = new List<IStrategy>();
            rnsAlterarEstoque.Add(valDadosEstoque);
            rnsAlterarEstoque.Add(complementoDtCadastro);
            List<IStrategy> rnsExcluirEstoque = new List<IStrategy>();
            List<IStrategy> rnsConsultarEstoque = new List<IStrategy>();
            /*
             * ESTOQUE - FIM ---------------------------------------------------------------------
             */

            /*
             * FORNECEDOR - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            //List<IStrategy> rnsSalvarFornecedor = new List<IStrategy>();
            //List<IStrategy> rnsAlterarFornecedor = new List<IStrategy>();
            //List<IStrategy> rnsExcluirFornecedor = new List<IStrategy>();
            List<IStrategy> rnsConsultarFornecedor = new List<IStrategy>();
            /*
             * FORNECEDOR - FIM ---------------------------------------------------------------------
             */

            /*
             * CUPOM - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            List<IStrategy> rnsSalvarCupom = new List<IStrategy>();
            List<IStrategy> rnsAlterarCupom = new List<IStrategy>();
            //List<IStrategy> rnsExcluirCupom = new List<IStrategy>();
            List<IStrategy> rnsConsultarCupom = new List<IStrategy>();
            /*
             * CUPOM - FIM ---------------------------------------------------------------------
             */

            /*
             * TipoCupom - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            //List<IStrategy> rnsSalvarTipoCupom = new List<IStrategy>();
            //List<IStrategy> rnsAlterarTipoCupom = new List<IStrategy>();
            //List<IStrategy> rnsExcluirTipoCupom = new List<IStrategy>();
            List<IStrategy> rnsConsultarTipoCupom = new List<IStrategy>();
            /*
             * TipoCupom - FIM ---------------------------------------------------------------------
             */

            /*
             * CLIENTE X CUPOM - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            List<IStrategy> rnsSalvarClienteCupom = new List<IStrategy>();
            //List<IStrategy> rnsAlterarClienteCupom = new List<IStrategy>();
            //List<IStrategy> rnsExcluirClienteCupom = new List<IStrategy>();
            List<IStrategy> rnsConsultarClienteCupom = new List<IStrategy>();
            /*
             * CLIENTE X CUPOM - FIM ---------------------------------------------------------------------
             */

            /*
             * StatusPedido - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            //List<IStrategy> rnsSalvarStatusPedido = new List<IStrategy>();
            //List<IStrategy> rnsAlterarStatusPedido = new List<IStrategy>();
            //List<IStrategy> rnsExcluirStatusPedido = new List<IStrategy>();
            List<IStrategy> rnsConsultarStatusPedido = new List<IStrategy>();
            /*
             * StatusPedido - FIM ---------------------------------------------------------------------
             */

            /*
             * CCPedido - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            //List<IStrategy> rnsSalvarCCPedido = new List<IStrategy>();
            //List<IStrategy> rnsAlterarCCPedido = new List<IStrategy>();
            //List<IStrategy> rnsExcluirCCPedido = new List<IStrategy>();
            List<IStrategy> rnsConsultarCCPedido = new List<IStrategy>();
            /*
             * CCPedido - FIM ---------------------------------------------------------------------
             */

            /*
             * PedidoDetalhe - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            //List<IStrategy> rnsSalvarPedidoDetalhe = new List<IStrategy>();
            //List<IStrategy> rnsAlterarPedidoDetalhe = new List<IStrategy>();
            //List<IStrategy> rnsExcluirPedidoDetalhe = new List<IStrategy>();
            List<IStrategy> rnsConsultarPedidoDetalhe = new List<IStrategy>();
            /*
             * PedidoDetalhe - FIM ---------------------------------------------------------------------
             */

            /*
             * Pedido - COMEÇO DA CRIAÇÃO DA LISTA DE STRATEGYS----------------------------------
             */
            List<IStrategy> rnsSalvarPedido = new List<IStrategy>();
            rnsSalvarPedido.Add(complementoDtCadastro);
            rnsSalvarPedido.Add(valDadosPedido);
            rnsSalvarPedido.Add(valStatusPedido);
            List<IStrategy> rnsAlterarPedido = new List<IStrategy>();
            rnsAlterarPedido.Add(complementoDtCadastro); 
            rnsAlterarPedido.Add(valStatusPedido);
            rnsAlterarPedido.Add(valAtualizaPedido);
            //List<IStrategy> rnsExcluirPedido = new List<IStrategy>();
            List<IStrategy> rnsConsultarPedido = new List<IStrategy>();
            /*
             * Pedido - FIM ---------------------------------------------------------------------
             */

            #endregion

            #region CRIAÇÃO DA LISTA DE REGRAS PARA CADA OPERAÇÂO

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
            rnsClienteEndereco.Add("CONSULTAR", rnsConsultarClienteEndereco);
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
            rnsClienteCartao.Add("CONSULTAR", rnsConsultarClienteCartao);
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

            /*
             * CategoriaMotivo - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsCategoriaMotivo = new Dictionary<string, List<IStrategy>>();
            //rnsCategoriaMotivo.Add("SALVAR", rnsSalvarCategoriaMotivo);
            //rnsCategoriaMotivo.Add("ALTERAR", rnsAlterarCategoriaMotivo);
            //rnsCategoriaMotivo.Add("EXCLUIR", rnsExcluirCategoriaMotivo);
            rnsCategoriaMotivo.Add("CONSULTAR", rnsConsultarCategoriaMotivo);
            /*
             * CategoriaMotivo - FIM ----------------------------------------------------------------------------
             */

            /*
             * CategoriaLivro - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsCategoriaLivro = new Dictionary<string, List<IStrategy>>();
            //rnsCategoriaLivro.Add("SALVAR", rnsSalvarCategoriaLivro);
            //rnsCategoriaLivro.Add("ALTERAR", rnsAlterarCategoriaLivro);
            //rnsCategoriaLivro.Add("EXCLUIR", rnsExcluirCategoriaLivro);
            rnsCategoriaLivro.Add("CONSULTAR", rnsConsultarCategoriaLivro);
            /*
             * CategoriaLivro - FIM ----------------------------------------------------------------------------
             */

            /*
             * EDITORA - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsEditora = new Dictionary<string, List<IStrategy>>();
            //rnsEditora.Add("SALVAR", rnsSalvarEditora);
            //rnsEditora.Add("ALTERAR", rnsAlterarEditora);
            //rnsEditora.Add("EXCLUIR", rnsExcluirEditora);
            rnsEditora.Add("CONSULTAR", rnsConsultarEditora);
            /*
             * EDITORA - FIM ----------------------------------------------------------------------------
             */

            /*
             * LIVRO - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsLivro = new Dictionary<string, List<IStrategy>>();
            //rnsLivro.Add("SALVAR", rnsSalvarLivro);
            rnsLivro.Add("ALTERAR", rnsAlterarLivro);
            //rnsLivro.Add("EXCLUIR", rnsExcluirLivro);
            rnsLivro.Add("CONSULTAR", rnsConsultarLivro);
            /*
             * LIVRO - FIM ----------------------------------------------------------------------------
             */

            /*
             * ESTOQUE - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsEstoque = new Dictionary<string, List<IStrategy>>();
            rnsEstoque.Add("SALVAR", rnsSalvarEstoque);
            rnsEstoque.Add("ALTERAR", rnsAlterarEstoque);
            rnsEstoque.Add("EXCLUIR", rnsExcluirEstoque);
            rnsEstoque.Add("CONSULTAR", rnsConsultarEstoque);
            /*
             * ESTOQUE - FIM ----------------------------------------------------------------------------
             */

            /*
             * FORNECEDOR - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsFornecedor = new Dictionary<string, List<IStrategy>>();
            //rnsFornecedor.Add("SALVAR", rnsSalvarFornecedor);
            //rnsFornecedor.Add("ALTERAR", rnsAlterarFornecedor);
            //rnsFornecedor.Add("EXCLUIR", rnsExcluirFornecedor);
            rnsFornecedor.Add("CONSULTAR", rnsConsultarFornecedor);
            /*
             * FORNECEDOR - FIM ----------------------------------------------------------------------------
             */

            /*
             * Cupom - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsCupom = new Dictionary<string, List<IStrategy>>();
            rnsCupom.Add("SALVAR", rnsSalvarCupom);
            rnsCupom.Add("ALTERAR", rnsAlterarCupom);
            //rnsCupom.Add("EXCLUIR", rnsExcluirCupom);
            rnsCupom.Add("CONSULTAR", rnsConsultarCupom);
            /*
             * Cupom - FIM ----------------------------------------------------------------------------
             */

            /*
             * TipoCupom - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsTipoCupom = new Dictionary<string, List<IStrategy>>();
            //rnsTipoCupom.Add("SALVAR", rnsSalvarTipoCupom);
            //rnsTipoCupom.Add("ALTERAR", rnsAlterarTipoCupom);
            //rnsTipoCupom.Add("EXCLUIR", rnsExcluirTipoCupom);
            rnsTipoCupom.Add("CONSULTAR", rnsConsultarTipoCupom);
            /*
             * TipoCupom - FIM ----------------------------------------------------------------------------
             */

            /*
             * CLIENTE X CUPOM - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsClienteCupom = new Dictionary<string, List<IStrategy>>();
            rnsClienteCupom.Add("SALVAR", rnsSalvarClienteCupom);
            //rnsClienteCupom.Add("ALTERAR", rnsAlterarClienteCupom);
            //rnsClienteCupom.Add("EXCLUIR", rnsExcluirClienteCupom);
            rnsClienteCupom.Add("CONSULTAR", rnsConsultarClienteCupom);
            /*
             * CLIENTE X CUPOM - FIM ----------------------------------------------------------------------------
             */

            /*
             * StatusPedido - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsStatusPedido = new Dictionary<string, List<IStrategy>>();
            //rnsStatusPedido.Add("SALVAR", rnsSalvarStatusPedido);
            //rnsStatusPedido.Add("ALTERAR", rnsAlterarStatusPedido);
            //rnsStatusPedido.Add("EXCLUIR", rnsExcluirStatusPedido);
            rnsStatusPedido.Add("CONSULTAR", rnsConsultarStatusPedido);
            /*
             * StatusPedido - FIM ----------------------------------------------------------------------------
             */

            /*
             * CCPedido - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsCCPedido = new Dictionary<string, List<IStrategy>>();
            //rnsCCPedido.Add("SALVAR", rnsSalvarCCPedido);
            //rnsCCPedido.Add("ALTERAR", rnsAlterarCCPedido);
            //rnsCCPedido.Add("EXCLUIR", rnsExcluirCCPedido);
            rnsCCPedido.Add("CONSULTAR", rnsConsultarCCPedido);
            /*
             * CCPedido - FIM ----------------------------------------------------------------------------
             */

            /*
             * PedidoDetalhe - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsPedidoDetalhe = new Dictionary<string, List<IStrategy>>();
            //rnsPedidoDetalhe.Add("SALVAR", rnsSalvarPedidoDetalhe);
            //rnsPedidoDetalhe.Add("ALTERAR", rnsAlterarPedidoDetalhe);
            //rnsPedidoDetalhe.Add("EXCLUIR", rnsExcluirPedidoDetalhe);
            rnsPedidoDetalhe.Add("CONSULTAR", rnsConsultarPedidoDetalhe);
            /*
             * PedidoDetalhe - FIM ----------------------------------------------------------------------------
             */

            /*
             * Pedido - COMEÇO DA CRIAÇÃO DA LISTA DE REGAS PARA CADA OPERAÇÂO -------------------------
             */
            Dictionary<string, List<IStrategy>> rnsPedido = new Dictionary<string, List<IStrategy>>();
            rnsPedido.Add("SALVAR", rnsSalvarPedido);
            rnsPedido.Add("ALTERAR", rnsAlterarPedido);
            //rnsPedido.Add("EXCLUIR", rnsExcluirPedido);
            rnsPedido.Add("CONSULTAR", rnsConsultarPedido);
            /*
             * Pedido - FIM ----------------------------------------------------------------------------
             */

            #endregion

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
            rns.Add(typeof(CategoriaMotivo).Name, rnsCategoriaMotivo);
            rns.Add(typeof(Categoria).Name, rnsCategoriaLivro);
            rns.Add(typeof(Editora).Name, rnsEditora);
            rns.Add(typeof(Livro).Name, rnsLivro);
            rns.Add(typeof(Estoque).Name, rnsEstoque);
            rns.Add(typeof(Fornecedor).Name, rnsFornecedor);
            rns.Add(typeof(Cupom).Name, rnsCupom);
            rns.Add(typeof(TipoCupom).Name, rnsTipoCupom);
            rns.Add(typeof(PedidoXCupom).Name, rnsClienteCupom);
            rns.Add(typeof(StatusPedido).Name, rnsStatusPedido);
            rns.Add(typeof(CartaoCreditoPedido).Name, rnsCCPedido);
            rns.Add(typeof(PedidoDetalhe).Name, rnsPedidoDetalhe);
            rns.Add(typeof(Pedido).Name, rnsPedido);

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
            string msg = ExecutarRegras(entidade, "SALVAR");

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
            string msg = ExecutarRegras(entidade, "ALTERAR");

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
            string msg = ExecutarRegras(entidade, "CONSULTAR");

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
            string msg = ExecutarRegras(entidade, "EXCLUIR");

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
        private string ExecutarRegras(EntidadeDominio entidade, string operacao)
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
