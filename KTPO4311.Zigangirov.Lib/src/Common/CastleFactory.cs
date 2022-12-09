using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Text;

namespace KTPO4311.Zigangirov.Lib.src.Common
{
    public static class CastleFactory
    {
        //Контейнер
        public static IWindsorContainer container { get; private set; }
        static CastleFactory()
        {
            //создать объект контейнер
            container = new WindsorContainer();
        }
    }
}
