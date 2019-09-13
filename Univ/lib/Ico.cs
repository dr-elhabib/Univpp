using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Univ.modeldb;
using Ninject;
namespace Univ.lib
{
    public static class Ico

    {
        public static IKernel ikernel = new StandardKernel(); 
        public static void SetUp() {


            BindViewModel();
        }

        private static void BindViewModel()
        {
            ikernel.Bind<db>().ToConstant(new db());
            ikernel.Bind<Date>().ToConstant(new Date());
            ikernel.Bind<setting>().ToConstant(Ico.getValue<db>().GetUnivdb().settings.ToList().FirstOrDefault());
            ikernel.Bind<IO>().ToConstant(new IO());
        }
        public static T getValue<T>() {

            return ikernel.TryGet<T>();
        }
        public static void setValue<T>( T t)
        {

            ikernel.Bind<T>().ToConstant(t);
        }
        public static void ResetValue<T>(T t)
        {

            ikernel.Rebind<T>().ToConstant(t);
        }


    }
}
