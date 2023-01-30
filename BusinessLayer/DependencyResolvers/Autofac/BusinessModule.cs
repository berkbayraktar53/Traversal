using Autofac;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;

namespace BusinessLayer.DependencyResolvers.Autofac
{
    public class BusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AboutManager>().As<IAboutService>();
            builder.RegisterType<EfAboutDal>().As<IAboutDal>();

            builder.RegisterType<ClientManager>().As<IClientService>();
            builder.RegisterType<EfClientDal>().As<IClientDal>();

            builder.RegisterType<CommentManager>().As<ICommentService>();
            builder.RegisterType<EfCommentDal>().As<ICommentDal>();

            builder.RegisterType<ContactManager>().As<IContactService>();
            builder.RegisterType<EfContactDal>().As<IContactDal>();

            builder.RegisterType<DestinationManager>().As<IDestinationService>();
            builder.RegisterType<EfDestinationDal>().As<IDestinationDal>();

            builder.RegisterType<GuideManager>().As<IGuideService>();
            builder.RegisterType<EfGuideDal>().As<IGuideDal>();

            builder.RegisterType<NewsletterManager>().As<INewsletterService>();
            builder.RegisterType<EfNewsletterDal>().As<INewsletterDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
        }
    }
}