using Rila.Core.Services;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Rila.Core.Composing
{
    public class RegisterServicesComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<IArticleService, ArticleService>(Lifetime.Request);
        }
    }
}
