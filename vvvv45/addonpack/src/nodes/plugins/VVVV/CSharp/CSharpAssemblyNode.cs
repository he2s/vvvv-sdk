#region licence/info

//////project name
//vvvv plugin template

//////description
//basic vvvv node plugin template.
//Copy this an rename it, to write your own plugin node.

//////licence
//GNU Lesser General Public License (LGPL)
//english: http://www.gnu.org/licenses/lgpl.html
//german: http://www.gnu.de/lgpl-ger.html

//////language/ide
//C# sharpdevelop 

//////dependencies
//VVVV.PluginInterfaces.V1;
//VVVV.Utils.VColor;
//VVVV.Utils.VMath;

//////initial author
//vvvv group

#endregion licence/info

//use what you need
using System;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Drawing;
using System.Collections;
using VVVV.PluginInterfaces.V1;
using VVVV.Utils.VColor;
using VVVV.Utils.VMath;
//using dhx;

//the vvvv node namespace
namespace VVVV.Nodes
{
	//protected class Compiler
	//{
	
	//}
	
	
	//class definition
	public class PluginTemplate: IPlugin, IDisposable
    {	          	 
    	#region field declaration
    	
    	//the host (mandatory)
    	private IPluginHost FHost; 

    	// Track whether Dispose has been called.
   		private bool FDisposed = false;

   		//assembly of user code
   		protected Assembly FAss;
   		protected Object FDynamicNode;
   		protected MethodInfo FEvaluate;
   		

    	//input pin declaration
    	private IStringIn FCodeInput;
    	private IStringIn FReferencesInput;
		private IValueIn FCompileInput;
		    	
    	//output pin declaration
    	//private IValueOut FMyValueOutput;
    	private IStringOut FErrorOutput;
    	
    	//private dhx.CodeCompiler FCodeCompiler;
    	
    	#endregion field declaration
       
    	#region constructor/destructor
    	
        public PluginTemplate()
        {
			AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(MyResolveEventHandler);
		}
        
        private Assembly MyResolveEventHandler(object sender, ResolveEventArgs args) 
        {
      		return typeof(PluginInfo).Assembly;
        }

        
        // Implementing IDisposable's Dispose method.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
        	Dispose(true);
        	// Take yourself off the Finalization queue
        	// to prevent finalization code for this object
        	// from executing a second time.
        	GC.SuppressFinalize(this);
        }
        
        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected virtual void Dispose(bool disposing)
        {
        	// Check to see if Dispose has already been called.
        	if(!FDisposed)
        	{
        		if(disposing)
        		{
        			// Dispose managed resources.
        		}
        		// Release unmanaged resources. If disposing is false,
        		// only the following code is executed.
	        	
        		FHost.Log(TLogType.Debug, "PluginTemplate is being deleted");
        		
        		// Note that this is not thread safe.
        		// Another thread could start disposing the object
        		// after the managed resources are disposed,
        		// but before the disposed flag is set to true.
        		// If thread safety is necessary, it must be
        		// implemented by the client.
        	}
        	FDisposed = true;
        }

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~PluginTemplate()
        {
        	// Do not re-create Dispose clean-up code here.
        	// Calling Dispose(false) is optimal in terms of
        	// readability and maintainability.
        	Dispose(false);
        }
        #endregion constructor/destructor
        
        #region node name and infos
       
        //provide node infos 
        private static IPluginInfo FPluginInfo;
        public static IPluginInfo PluginInfo
	    {
	        get 
	        {
	        	if (FPluginInfo == null)
				{
					//fill out nodes info
					//see: http://www.vvvv.org/tiki-index.php?page=vvvv+naming+conventions
					FPluginInfo = new PluginInfo();
					
					//the nodes main name: use CamelCaps and no spaces
					FPluginInfo.Name = "CSharp";
					//the nodes category: try to use an existing one
					FPluginInfo.Category = "VVVV";
					//the nodes version: optional. leave blank if not
					//needed to distinguish two nodes of the same name and category
					FPluginInfo.Version = "CompileRun";
					
					//the nodes author: your sign
					FPluginInfo.Author = "vvvv group";
					//describe the nodes function
					FPluginInfo.Help = "compiles the c# code and runs it right away.";
					//specify a comma separated list of tags that describe the node
					FPluginInfo.Tags = "";
					
					//give credits to thirdparty code used
					FPluginInfo.Credits = "";
					//any known problems?
					FPluginInfo.Bugs = "";
					//any known usage of the node that may cause troubles?
					FPluginInfo.Warnings = "";
					
					//leave below as is
					System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(true);
					System.Diagnostics.StackFrame sf = st.GetFrame(0);
					System.Reflection.MethodBase method = sf.GetMethod();
					FPluginInfo.Namespace = method.DeclaringType.Namespace;
					FPluginInfo.Class = method.DeclaringType.Name;
					//leave above as is
				}
				return FPluginInfo;
	        }
		}

        public bool AutoEvaluate
        {
        	//return true if this node needs to calculate every frame even if nobody asks for its output
        	get {return false;}
        }
        
        #endregion node name and infos
        
      	#region pin creation
        
        //this method is called by vvvv when the node is created
        public void SetPluginHost(IPluginHost Host)
	    {
        	//assign host
	    	FHost = Host;
	    	//FCodeCompiler = new dhx.CodeCompiler();

	    	//create inputs	    	
	    	FHost.CreateStringInput("Code", TSliceMode.Single, TPinVisibility.True, out FCodeInput);
	    	FCodeInput.SetSubType("see help patch for example code", false);	

	    	FHost.CreateStringInput("References", TSliceMode.Dynamic, TPinVisibility.True, out FReferencesInput);
	    	FReferencesInput.SetSubType("System", false);	

	    	FHost.CreateValueInput("Compile", 1, null, TSliceMode.Dynamic, TPinVisibility.True, out FCompileInput);
	    	FCompileInput.SetSubType(0, 1, 1, 0, true, false, false);
	    	
	    	//create outputs	    	
	    	//FHost.CreateValueOutput("Value Output", 1, null, TSliceMode.Dynamic, TPinVisibility.True, out FMyValueOutput);
	    	//FMyValueOutput.SetSubType(double.MinValue, double.MaxValue, 0.01, 0, false, false, false);
	    	
	    	FHost.CreateStringOutput("Errors", TSliceMode.Single, TPinVisibility.True, out FErrorOutput);
	    	FErrorOutput.SetSubType("Not compiled yet", false);
        }

        #endregion pin creation
        
        #region mainloop
        
        public void Configurate(IPluginConfig Input)
        {
        	//nothing to configure in this plugin
        	//only used in conjunction with inputs of type cmpdConfigurate
        }
        
        //here we go, thats the method called by vvvv each frame
        //all data handling should be in here
        public void Evaluate(int SpreadMax)
        {     	
        	//if any of the inputs has changed
        	//recompute the outputs
        	//if (FCodeInput.PinIsChanged) or (FRefernces.PinIsChanged)
        	//{	
        	 			
        	double compile;
        	FCompileInput.GetValue(0, out compile);
        	if (compile == 1.0)
        	{
        		string code, reference;
        		FCodeInput.GetString(0, out code);
        		
        		ArrayList refs = new ArrayList();
        		int refcount = FReferencesInput.SliceCount;
        		for (int i=0; i<refcount; i++)
        		{
        		 	FReferencesInput.GetString(i, out reference); 
        		 	refs.Add(reference);
        		}        		
        		
        		string errs;
	      		Assembly FAss = BuildAssembly(code, refs, out errs);
	      		
	      		FErrorOutput.SetString(0, errs);
	      		if (errs == "")
	      		{
	      			FErrorOutput.SetString(0, "no errors");
	      			if (FAss != null)
	      			{
		      			FErrorOutput.SetString(0, "we have an ass");	      				
		      			
		      			FDynamicNode = null;
		      			FDynamicNode = FAss.CreateInstance(GetType().Namespace + ".DynamicNode");
		      			
		      			Type type = FDynamicNode.GetType();
				
		      			//Type type = FAss.GetType(GetType().Namespace + ".DynamicNode");
	      				//ConstructorInfo _DynamicNode = type.GetConstructor(Type.EmptyTypes);
	      				//FErrorOutput.SetString(0, "we have a constructor");	      				
		      			//FDynamicNode = _DynamicNode.Invoke(null);
		      			
		      			if (FDynamicNode == null)
		      					      				
		      			{
		      				FErrorOutput.SetString(0, "we have no node");
		      			}
		      			else
		      			{		      		
	      					FErrorOutput.SetString(0, "we have a node");	
	      					
	      					MethodInfo _SetPluginHost = type.GetMethod("SetPluginHost");
	      					//FErrorOutput.SetString(0,_SetPluginHost.GetMethodBody().ToString());
	      					
	      					if (_SetPluginHost != null)
	      					{
		      					FErrorOutput.SetString(0, "we have a pointer to method SetPluginHost");	
		      					try
		      					{
		      					_SetPluginHost.Invoke(FDynamicNode, new System.Object[]{FHost});
		      					//_SetPluginHost.Invoke(;
		      					}
		      					catch (Exception e)
		      					{
		      						FHost.Log(TLogType.Debug, "DynamicNode.Evaluate: " + e.Message + " " + e.Source + " " + e.HelpLink + " " + e.StackTrace);
		      					}
		      					//type.GetMethod("SetPluginHost").Invoke(FDynamicNode, null); //new System.Object[1]{FHost});
		      					FErrorOutput.SetString(0, "we called set plugin host");
		      					
		      					FEvaluate = type.GetMethod("Evaluate");
		      					FErrorOutput.SetString(0, "we retrieved evaluate");
	      					}
		      			}		      			
	      			}
	      		}
       		}   
        	
        	if (FEvaluate != null)
        	{
        	 	FEvaluate.Invoke(FDynamicNode, new Object[1]{SpreadMax});
        	}
        	 
			//the variables to fill with the input data
   	        //string currentStringSlice;

   	        //read data from inputs
        	//FMyStringInput.GetString(0, out currentStringSlice);
        		
        	//first set slicecounts for all outputs
        	//the incoming int SpreadMax is the maximum slicecount of all input pins, which is a good default
        	        	
        	//FMyStringOutput.SliceCount = SpreadMax;
        	//FMyValueOutput.SliceCount = 1;
        	//FMyValueOutput.SetValue(0, FCodeCompiler.CalculateDouble(currentStringSlice));
        	
        	//loop for all slices
        	//for (int i=0; i<SpreadMax; i++)
        	//{
        	
        	//write data to outputs
        	//	FMyStringOutput.SetString(i, currentStringSlice);
        	//}       	 
        }
             
        #endregion mainloop  
        
		private Assembly BuildAssembly(string code, ArrayList references, out string err)
		{
			Microsoft.CSharp.CSharpCodeProvider provider = new CSharpCodeProvider();
			ICodeCompiler compiler = provider.CreateCompiler();
			CompilerParameters compilerparams = new CompilerParameters();
			compilerparams.GenerateExecutable = false;
			compilerparams.GenerateInMemory = true;
			
			foreach (string reference in references)
			{
				compilerparams.ReferencedAssemblies.Add(reference + ".dll");				
				FHost.Log(TLogType.Debug, reference + ".dll");
			}
			
			
				
			//compilerparams.ReferencedAssemblies.Add("System.dll");
			//compilerparams.ReferencedAssemblies.Add("System.Windows.Forms.dll");
			CompilerResults results = compiler.CompileAssemblyFromSource(compilerparams, code);
			if (results.Errors.HasErrors)
			{
				StringBuilder errors = new StringBuilder("Compiler Errors :\r\n");
				foreach (CompilerError error in results.Errors )
				{
					errors.AppendFormat("Line {0},{1}\t: {2}\n", error.Line, error.Column, error.ErrorText);
				}
				err = errors.ToString();
				return null;
			}
			else
			{
				err = "";
				return results.CompiledAssembly;
			}
		}
	}
}
