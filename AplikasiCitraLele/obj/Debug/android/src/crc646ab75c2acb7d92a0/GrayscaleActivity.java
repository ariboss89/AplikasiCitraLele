package crc646ab75c2acb7d92a0;


public class GrayscaleActivity
	extends androidx.appcompat.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("AplikasiCitraLele.Activity.GrayscaleActivity, AplikasiCitraLele", GrayscaleActivity.class, __md_methods);
	}


	public GrayscaleActivity ()
	{
		super ();
		if (getClass () == GrayscaleActivity.class) {
			mono.android.TypeManager.Activate ("AplikasiCitraLele.Activity.GrayscaleActivity, AplikasiCitraLele", "", this, new java.lang.Object[] {  });
		}
	}


	public GrayscaleActivity (int p0)
	{
		super (p0);
		if (getClass () == GrayscaleActivity.class) {
			mono.android.TypeManager.Activate ("AplikasiCitraLele.Activity.GrayscaleActivity, AplikasiCitraLele", "System.Int32, mscorlib", this, new java.lang.Object[] { p0 });
		}
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
