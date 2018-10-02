package md53cf738c0d2d67ae652708e85e9e4146a;


public class Tela3_Novo_Usuario
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("CAM_SME.Tela3_Novo_Usuario, CAM_SME, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Tela3_Novo_Usuario.class, __md_methods);
	}


	public Tela3_Novo_Usuario ()
	{
		super ();
		if (getClass () == Tela3_Novo_Usuario.class)
			mono.android.TypeManager.Activate ("CAM_SME.Tela3_Novo_Usuario, CAM_SME, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
