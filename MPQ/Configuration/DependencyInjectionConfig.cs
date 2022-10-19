using Microsoft.Extensions.DependencyInjection;
using MPQ.Helpers;
using MPQ.Utils.Cache;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MPQ.Utils.I18n;
using MPQ.Data.Repositories;
using MPQ.Utils.Permissao;

namespace MPQ.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependenciesServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISiteRepository, SiteRepository>();
            services.AddScoped<IUserSiteRepository, UserSiteRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IGroupMenuRepository, GroupMenuRepository>();
            services.AddScoped<IUserGroupRepository, UserGroupRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IGoalRepository, GoalRepository>();
            services.AddScoped<IIndicatorRepository, IndicatorRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProductionLineRepository, ProductionLineRepository>();
            services.AddScoped<IGoalProductionLineRepository, GoalProductionLineRepository>();
            services.AddScoped<IProductionAreaRepository, ProductionAreaRepository>();
            services.AddScoped<IGoalStfRepository, GoalStfRepository>();
            services.AddScoped<IBusinessUnitRepository, BusinessUnitRepository>();
            services.AddScoped<IGoalCustomerRepository, GoalCustomerRepository>();
            services.AddScoped<IGoalCustomerRangeRepository, GoalCustomerRangeRepository>();
            services.AddScoped<IComparisonOperatorRepository, ComparisonOperatorRepository>();
            services.AddScoped<IGoalProductionLineRangeRepository, GoalProductionLineRangeRepository>();
            services.AddScoped<IProductionAreaRepository, ProductionAreaRepository>();
            services.AddScoped<IProducedPartRepository, ProducedPartRepository>();
            services.AddScoped<IComplaintRepository, ComplaintRepository>();
            services.AddScoped<IComplaintWarrantyPartRepository, ComplaintWarrantyPartRepository>();
            services.AddScoped<ICustomerViewRepository, CustomerViewRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IUsuarioAplicacao, UsuarioAplicacaoSession>();
            services.AddTransient<IAplicacao, Aplicacao>();
            services.AddTransient<ISessionHelper, SessionHelper>();            
            services.AddTransient<ICultureFactory, CultureFactory>();
            services.AddTransient<IAutenticacaoHelper, AutenticacaoCookieHelper>();
            services.AddTransient<IPermissaoHelper, PermissaoHelper>();
            services.AddTransient<IExcelHelper, ExcelHelper>();
            services.AddTransient<IUIHelper, UIHelper>();
            services.AddTransient<IUploadArquivo, SalvarArquivoLocalHelper>();

            //services.AddTransient<IDominioServices, DominioServices>();
            //services.AddTransient<IEmpresaServices, EmpresaServices>();
            //services.AddTransient<IIdiomaServices, IdiomaServices>();
            //services.AddTransient<IPermissaoServices, PermissaoServices>();
            //services.AddTransient<IPlantaServices, PlantaServices>();
            //services.AddTransient<IUsuarioService, UsuarioServices>();
            //services.AddTransient<IRotaServices, RotaServices>();
            //services.AddTransient<ISistemaServices, SistemaServices>();
            //services.AddTransient<IGrupoPermissaoServices, GrupoPermissaoServices>();
            //services.AddTransient<ILogSolicitacaoService, LogSolicitacaoService>();
            //services.AddTransient<ICadastrosPadrao, CadastrosPadrao>();
            //services.AddTransient<ISolicitacaoService, SolicitacaoService>();
            //services.AddTransient<IStatusService, StatusService>();
            //services.AddSingleton<IChecklistServices, ChecklistServices>();
            //services.AddSingleton<IChecklistMetodoServices, ChecklistMetodoServices>();
            //services.AddSingleton<IChecklistItemServices, ChecklistItemServices>();
            //services.AddSingleton<IChecklistTipoServices, ChecklistTipoServices>();
            //services.AddSingleton<ICheckListModeloPlantaService, CheckListModeloPlantaService>();
            //services.AddSingleton<IRelacionamentoChecklistService, RelacionamentoChecklistService>();
            //services.AddTransient<ICategoriaService, CategoriaService>();
            //services.AddTransient<ILinhaService, LinhaService>();
            //services.AddTransient<IProcessoService, ProcessoService>();
            //services.AddTransient<IAreaServices, AreaServices>();
            //services.AddTransient<ITipoAreaServices, TipoAreaServices>();
            //services.AddTransient<IParadaServices, ParadaServices>();
            //services.AddTransient<IParadaLinhaTurnoService, ParadaLinhaTurnoService>();

            //services.AddTransient<IEventoService, EventoService>();
            //services.AddTransient<IEventoAcaoService, EventoAcaoService>();
            //services.AddTransient<IEquipamentoService, EquipamentoService>();
            //services.AddTransient<IClienteService, ClienteService>();
            //services.AddTransient<IEquipamentoClienteService, EquipamentoClienteService>();
            //services.AddTransient<ILinhaPostoService, LinhaPostoService>();
            //services.AddTransient<IUnidadeNegocioServices, UnidadeNegocioServices>();
            //services.AddTransient<IHenkatenService, HenkatenService>();
            //services.AddTransient<IHenkatenEventoService, HenkatenEventoService>();


            ////ConsultaChecklist
            //services.AddTransient<IChecklistTipoServices, ChecklistTipoServices>();
            //services.AddTransient<IPlantaServices, PlantaServices>();
            //services.AddTransient<ILinhaService, LinhaService>();
            //services.AddTransient<ITurnoService, TurnoService>();
            //services.AddTransient<IChecklistServices, ChecklistServices>();
            //services.AddTransient<IChecklistRespostaService, ChecklistRespostaService>();
            //services.AddTransient<IChecklistMonitoramentoRevisaoService, ChecklistMonitoramentoRevisaoService>();
            //services.AddTransient<IStatusSistemaService, StatusSistemaService>();            
            //services.AddTransient<IEquipamentoService, EquipamentoService>();
            //services.AddTransient<IProcessoService, ProcessoService>();
            //services.AddTransient<ICatalogoDefeitoServices, CatalogoDefeitoServices>();
            //services.AddTransient<IClassificacaoRetrabalhoServices, ClassificacaoRetrabalhoServices>();
            //services.AddTransient<ICatalogoDefeitoComponenteServices, CatalogoDefeitoComponenteServices>();
            //services.AddTransient<IPermissaoSistemaServices, PermissaoSistemaService>();
            //services.AddTransient<IPerfilAcessoServices, PerfilAcessoService>();
            //services.AddTransient<IMenuService, MenuService>();
            //services.AddTransient<IPermissaoUsuarioService, PermissaoUsuarioService>();
            //services.AddTransient<IUsuarioPerfilAcessoService, UsuarioPerfilAcessoService>();
            //services.AddTransient<IPermissaoV2Service, PermissaoV2Service>();
            //services.AddTransient<ICombinacaoService, CombinacaoService>();
            //services.AddTransient<IModeloMaquinaServices, ModeloMaquinaServices>();
            //services.AddTransient<ITempoSwtService, TempoSwtService>();
            //services.AddTransient<ICombinacaoV2Service, CombinacaoV2Service>();
            //services.AddTransient<ITipoEtiquetaService, TipoEtiquetaService>();
            //services.AddTransient<ILogAplicacao, LogAplicacao>();
            //services.AddTransient<IImportacaoServices, ImportacaoServices>();
            //services.AddTransient<IImportacao, TempoSwtService>();
            //services.AddTransient<IAplicadorService, AplicadorService>();
            //services.AddTransient<IAplicadorDetalhesService, AplicadorDetalhesService>();
            //services.AddTransient<ITipoIntegracaoService, TipoIntegracaoService>();
            //services.AddTransient<ILayoutEtiquetaService, LayoutEtiquetaService>();
            //services.AddTransient<ITipoEtiquetaIntegracaoService, TipoEtiquetaIntegracaoService>();
            //services.AddTransient<IIntegracaoService, IntegracaoService>();
            //services.AddTransient<IMatrizDefeitoProcessoService, MatrizDefeitoProcessoService>();
            //services.AddTransient<IMatrizDefeitoIdiomaService, MatrizDefeitoIdiomaService>();
            //services.AddTransient<IAtividadeService, AtividadeService>();
            //services.AddTransient<IClasseMpOperacaoService, ClasseMpOperacaoService>();
            //services.AddTransient<IProcessoOperacaoService, ProcessoOperacaoService>();
            //services.AddTransient<IOperacaoService, OperacaoService>();
            //services.AddTransient<IClasseMpService, ClasseMpService>();
            //services.AddTransient<IModeloPacoService, ModeloPacoService>();
            //services.AddTransient<IItemService, ItemService>();
            //services.AddTransient<IToleranciaService, ToleranciaService>();
            //services.AddTransient<ITipoAlimentacaoService, TipoAlimentacaoService>();
            //services.AddTransient<ITipoFerramentalService, TipoFerramentalService>();
            //services.AddTransient<IModeloAplicadorService, ModeloAplicadorService>();
            //services.AddTransient<ICravacaoIsolanteService, CravacaoIsolanteService>();
            //services.AddTransient<IProvePinService, ProvePinService>();
            //services.AddTransient<IPadraoCravacaoService, PadraoCravacaoService>();
            //services.AddTransient<IAplicadorHistoricoService, AplicadorHistoricoService>();
            //services.AddTransient<ITerminalService, TerminalService>();
            //services.AddTransient<IPadraoCravacaoDadosTerminalService, PadraoCravacaoDadosTerminalService>();
            //services.AddTransient<IPadraoCravacaoDadosCaboService, PadraoCravacaoDadosCaboService>();
            //services.AddTransient<IPadraoCravacaoDadosCaboPlantaClienteService, PadraoCravacaoDadosCaboPlantaClienteService>();
            //services.AddTransient<IPadraoCravacaoDadosTerminalHistoricoService, PadraoCravacaoDadosTerminalHistoricoService>();
            //services.AddTransient<IPadraoCravacaoDadosCaboHistoricoService, PadraoCravacaoDadosCaboHistoricoService>();
            //services.AddTransient<IClasseSaService, ClasseSaService>();
            //services.AddTransient<IClasseSaOperacaoService, ClasseSaOperacaoService>();            

            return services;
        }
    }
}
