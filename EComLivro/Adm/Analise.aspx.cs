using Dominio.Livro;
using Dominio.Venda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EComLivro.Adm
{
    public partial class Analise : ViewGenerico
    {
        Dominio.Analise.Analise analise = new Dominio.Analise.Analise();

        protected string xAxis = "";
        protected string series = "";

        protected override void Page_Load(object sender, EventArgs e)
        {
            ConstruirGrafico();
        }

        protected void btnFiltraGrafico_Click(object sender, EventArgs e)
        {
            ConstruirGrafico();
        }

        private void ConstruirGrafico()
        {
            analise = new Dominio.Analise.Analise();
            int evade = 0;

            if (!txtDtInicio.Text.Equals(""))
            {
                string[] data = txtDtInicio.Text.Split('-');
                analise.DataCadastro = new DateTime(Convert.ToInt32(data[0].ToString()), Convert.ToInt32(data[1].ToString()), Convert.ToInt32(data[2].ToString()));
            }

            if (!txtDtFim.Text.Equals(""))
            {
                string[] data = txtDtFim.Text.Split('-');
                analise.DataFim = new DateTime(Convert.ToInt32(data[0].ToString()), Convert.ToInt32(data[1].ToString()), Convert.ToInt32(data[2].ToString()) + 1);
            }

            entidades = commands["CONSULTAR"].execute(analise).Entidades;
            try
            {
                evade = entidades.Count;
            }
            catch
            {
                evade = 0;
            }

            StringBuilder conteudo = new StringBuilder();
            List<Pedido> pedidos = new List<Pedido>();

            // lista para conter todos clientes retornados do BD
            foreach (Pedido pedidoAux in commands["CONSULTAR"].execute(analise).Entidades)
            {
                // para pesquisar os itens que o pedido tem

                // passa ID de pedido e consulta
                foreach (PedidoDetalhe detalhe in
                    commands["CONSULTAR"].execute(new PedidoDetalhe { IdPedido = pedidoAux.ID }).Entidades)
                {
                    // consulta as categorias dos livros
                    foreach (LivroXCategoria categoria in
                    commands["CONSULTAR"].execute(new LivroXCategoria { ID = detalhe.Livro.ID }).Entidades)
                    {
                        // Passa categorias para o livro do detalhe
                        detalhe.Livro.Categorias.Add(categoria.Categoria);
                    }

                    // Passa itens para o pedido
                    pedidoAux.Detalhes.Add(detalhe);
                }

                pedidos.Add(pedidoAux);
            }

            int mes = 0;
            int ano = 0;

            pedidos = pedidos.OrderBy(p => p.DataCadastro).ToList();

            mes = pedidos[0].DataCadastro.Value.Month;
            ano = pedidos[0].DataCadastro.Value.Year;

            // Setando xAxis do Highcharts
            xAxis = "";
            xAxis += "categories: [" + "'" + (mes + "/" + ano).ToString() + "'";
            for (int i = 0; i < pedidos.Count; i++)
            {
                // verifica se mês e ano é igual 
                if (pedidos[i].DataCadastro.Value.Month != mes || pedidos[i].DataCadastro.Value.Year != ano)
                {
                    mes = pedidos[i].DataCadastro.Value.Month;
                    ano = pedidos[i].DataCadastro.Value.Year;

                    xAxis += ", '" + (mes + "/" + ano).ToString() + "'";
                }
            }
            xAxis += "]";

            // lista para conter todas as categorias do sistema para ser setado em "series" do highCharts
            List<Categoria> listaCategorias = commands["CONSULTAR"].execute(new Categoria()).Entidades.Cast<Categoria>().ToList();

            series = "";

            string SerieCategoria1 = "";
            string SerieCategoria2 = "";
            string SerieCategoria3 = "";
            string SerieCategoria4 = "";
            string SerieCategoria5 = "";

            int qtdeCategoria1 = 0;
            int qtdeCategoria2 = 0;
            int qtdeCategoria3 = 0;
            int qtdeCategoria4 = 0;
            int qtdeCategoria5 = 0;

            bool flgHouveTroca = false;

            int mesAnt = 0;
            int anoAnt = 0;

            mes = pedidos[0].DataCadastro.Value.Month;
            ano = pedidos[0].DataCadastro.Value.Year;

            // montando series
            for (int i = 0; i < pedidos.Count; i++)
            {
                for (int j = 0; j < pedidos[i].Detalhes.Count; j++)
                {
                    for (int k = 0; k < pedidos[i].Detalhes[j].Livro.Categorias.Count; k++)
                    {
                        // verifica se mês e ano é igual 
                        if (pedidos[i].DataCadastro.Value.Month == mes && pedidos[i].DataCadastro.Value.Year == ano)
                        { // se for mesmo mês e ano adiciona a quantidade
                            switch (pedidos[i].Detalhes[j].Livro.Categorias[k].ID)
                            {
                                case 1:
                                    qtdeCategoria1 += pedidos[i].Detalhes[j].Quantidade;
                                    break;
                                case 2:
                                    qtdeCategoria2 += pedidos[i].Detalhes[j].Quantidade;
                                    break;
                                case 3:
                                    qtdeCategoria3 += pedidos[i].Detalhes[j].Quantidade;
                                    break;
                                case 4:
                                    qtdeCategoria4 += pedidos[i].Detalhes[j].Quantidade;
                                    break;
                                case 5:
                                    qtdeCategoria5 += pedidos[i].Detalhes[j].Quantidade;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        { // se não for mesmo mês e ano, fecha a quantidade e passa valor para série e zera a quantidade
                            SerieCategoria1 += qtdeCategoria1.ToString() + ", ";
                            SerieCategoria2 += qtdeCategoria2.ToString() + ", ";
                            SerieCategoria3 += qtdeCategoria3.ToString() + ", ";
                            SerieCategoria4 += qtdeCategoria4.ToString() + ", ";
                            SerieCategoria5 += qtdeCategoria5.ToString() + ", ";
                            qtdeCategoria1 = 0;
                            qtdeCategoria2 = 0;
                            qtdeCategoria3 = 0;
                            qtdeCategoria4 = 0;
                            qtdeCategoria5 = 0;

                            //adiciona a quantidade
                            switch (pedidos[i].Detalhes[j].Livro.ID)
                            {
                                case 1:
                                    qtdeCategoria1 += pedidos[i].Detalhes[j].Quantidade;
                                    break;
                                case 2:
                                    qtdeCategoria2 += pedidos[i].Detalhes[j].Quantidade;
                                    break;
                                case 3:
                                    qtdeCategoria3 += pedidos[i].Detalhes[j].Quantidade;
                                    break;
                                case 4:
                                    qtdeCategoria4 += pedidos[i].Detalhes[j].Quantidade;
                                    break;
                                case 5:
                                    qtdeCategoria5 += pedidos[i].Detalhes[j].Quantidade;
                                    break;
                                default:
                                    break;
                            }

                            mesAnt = mes;
                            anoAnt = ano;
                            mes = pedidos[i].DataCadastro.Value.Month;
                            ano = pedidos[i].DataCadastro.Value.Year;

                            flgHouveTroca = true;
                        }
                    }
                }


                if (i == pedidos.Count - 1 && mesAnt != mes && anoAnt == ano)
                {
                    SerieCategoria1 += qtdeCategoria1.ToString();
                    SerieCategoria2 += qtdeCategoria2.ToString();
                    SerieCategoria3 += qtdeCategoria3.ToString();
                    SerieCategoria4 += qtdeCategoria4.ToString();
                    SerieCategoria5 += qtdeCategoria5.ToString();
                } else if (i == pedidos.Count - 1 && mesAnt != mes && anoAnt != ano)
                {
                    SerieCategoria1 += qtdeCategoria1.ToString();
                    SerieCategoria2 += qtdeCategoria2.ToString();
                    SerieCategoria3 += qtdeCategoria3.ToString();
                    SerieCategoria4 += qtdeCategoria4.ToString();
                    SerieCategoria5 += qtdeCategoria5.ToString();
                }
            }

            // setando series
            for (int i = 0; i < listaCategorias.Count; i++)
            {
                series += "{" +
                                "name: '" + listaCategorias[i].Nome + "',";

                switch (i)
                {
                    case 0:
                        series += "data: [" + SerieCategoria1 + "]";
                        break;
                    case 1:
                        series += "data: [" + SerieCategoria2 + "]";
                        break;
                    case 2:
                        series += "data: [" + SerieCategoria3 + "]";
                        break;
                    case 3:
                        series += "data: [" + SerieCategoria4 + "]";
                        break;
                    case 4:
                        series += "data: [" + SerieCategoria5 + "]";
                        break;
                    default:
                        break;
                }

                series += "}";


                if (i != listaCategorias.Count - 1)
                {
                    series += ", ";
                }
            }

            // Rodapé da tabela informativo de quando foi a última vez que foi atualizada a lista
            lblRodaPeTabela.InnerText = "Gráfico atualizado em " + DateTime.Now.ToString();
        }
    }
}