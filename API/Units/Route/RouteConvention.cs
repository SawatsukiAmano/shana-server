using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
namespace API.Units
{
    public class RouteConvention : IApplicationModelConvention
    {
        /// <summary>
        /// 定义一个变量，保存路由前缀
        /// </summary>
        private readonly AttributeRouteModel _centralPrefix;

        /// <summary>
        /// 调用时传入指定的路由前缀
        /// </summary>
        /// <param name="routeTemplateProvider"></param>
        public RouteConvention(IRouteTemplateProvider routeTemplateProvider)
        {
            _centralPrefix = new AttributeRouteModel(routeTemplateProvider);
        }

        //实现Apply方法
        public void Apply(ApplicationModel application)
        {
            //遍历所有Controller
            foreach (var controller in application.Controllers)
            {
                //给路由添加前缀，如果在控制器中已经标注有路由了，则会在路由的前面再添加指定的路由内容。

                // 1. 查找已经标记了RouteAttribute的控制器
                var matchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel != null).ToList();
                if (matchedSelectors.Any())
                {
                    foreach (var selectorModel in matchedSelectors)
                    {
                        // 在已有的路由上再添加一个路由前缀
                        selectorModel.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_centralPrefix,
                            selectorModel.AttributeRouteModel);
                        Console.WriteLine(selectorModel.AttributeRouteModel);
                    }
                }

                //2. 没有标记RouteAttribute的控制器
                var unmatchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel == null).ToList();
                if (unmatchedSelectors.Any())
                {
                    foreach (var selectorModel in unmatchedSelectors)
                    {
                        //添加一个路由前缀
                        selectorModel.AttributeRouteModel = _centralPrefix;
                    }
                }
            }
        }
    }
}
