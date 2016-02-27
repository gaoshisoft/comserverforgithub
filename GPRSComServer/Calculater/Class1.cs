//这个命名空间不要忘了  
using System;
using System.CodeDom.Compiler;
using System.Reflection;

namespace Calculater
{
    /// <summary>  
    /// 动态求值  
    /// </summary>  
    public class Evaluator
    {
        /// <summary>  
        /// 计算结果,如果表达式出错则抛出异常  
        /// </summary>  
        /// <param name="statement">表达式,如"1+2+3+4"</param>  
        /// <returns>结果</returns>  
        public static object Eval(string statement)
        {
            //调用实例化后的对象的方法，注意：第一个参数必须为JScript源代码里的方法。且所传字符串里的运算规则或方法也必须为JScript所支持  
            return _evaluatorType.InvokeMember(
                        "Eval",
                        BindingFlags.InvokeMethod,
                        null,
                        _evaluator,
                        new object[] { statement }
                     );
        }

        static Evaluator()
        {
            //构造JScript的编译驱动代码，该语言必须为.net支持的语言，根据语言名称驱动对应的编译器  
            CodeDomProvider provider = CodeDomProvider.CreateProvider("JScript");
            //设置调用编译器的参数,下面参数设置编译器将程序集输出到内存。  
            CompilerParameters parameters;
            parameters = new CompilerParameters();
            parameters.GenerateInMemory = true;
            //根据设置的编译器参数对语言源代码进行编译  
            CompilerResults results;
            results = provider.CompileAssemblyFromSource(parameters, _jscriptSource);
            //获取编译好的程序集  
            Assembly assembly = results.CompiledAssembly;
            //获取程序集后，使用反射获取程序集中的类  
            _evaluatorType = assembly.GetType("Evaluator");
            //创建获取到的类的实例  
            _evaluator = Activator.CreateInstance(_evaluatorType);
        }

        private static object _evaluator = null;
        private static Type _evaluatorType = null;

        /// <summary>  
        /// JScript语言源代码。即你想要被编译的源代码，这个代码里包含了字符串参数中方法或规则。  
        /// </summary>  
        private static readonly string _jscriptSource =
            @" import SharpDll;  
                class Evaluator  
               {  
                   public function Eval(expr : String) : String   
                   {   
                      return eval(expr);   
                   }  
                public function RoundUp(numVar:double,len:int):double  
                {  //var dindex:int = numVar.ToString().IndexOf(" + "\".\"" + @");  
                   //return double.Parse(numVar.ToString().Substring(0,dindex))+ 1;  
                    var numRev:double = 0;  
                    var dindex:int = numVar.ToString().IndexOf(" + "\".\"" + @");  
                    if (dindex> -1)  
                    {  
                        if (len == 0)  
                        {  
                            if (int.Parse(numVar.ToString().Substring(dindex+1))> 0)  
                            {  
                                numRev = double.Parse(numVar.ToString().Substring(0,dindex))+ 1;  
                            }  
                        }  
                        if (len > 0)  
                        {  
                            if ((dindex + len) > numVar.ToString().Substring(dindex).Length)  
                            {  
                                numRev = numVar;  
                            }  
                            else  
                            {  
                                if (int.Parse(numVar.ToString().Substring(dindex + len+1)) > 0)  
                                {  
                                    var tempnum:double = double.Parse(numVar.ToString().Substring(0,dindex+len+1)) * invokeIn(len)+1;  
                                    numRev = double.Parse(tempnum.ToString().Insert(tempnum.ToString().Length - len, " + "\".\"" + @"));  
                                }  
                                else   
                                {  
                                    numRev = double.Parse(numVar.ToString().Substring(0, dindex + len));  
                                }  
                            }  
                        }  
                    }  
                    else   
                    {  
                        numRev = numVar;  
                    }  
                    return numRev;  
                }  
                public function invokeInt(len:int):int  
                {  
                    var TempVal:String = " + "\"1\"" + @";  
                    var i:int = 0;  
                    while(i<len)   
                    {  
                        TempVal = TempVal+" + "\"0\"" + @";  
                        i++;  
                    }  
                    return int.Parse(TempVal);  
                }  
               }";
    }
}


//该代码片段来自于: http://www.sharejs.com/codes/csharp/7131