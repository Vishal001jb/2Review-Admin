package md546d1593b92d4bdafaf578b0dba851811;


public class AdapterEmployeeViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Login.Adapter.AdapterEmployeeViewHolder, Login, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AdapterEmployeeViewHolder.class, __md_methods);
	}


	public AdapterEmployeeViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == AdapterEmployeeViewHolder.class)
			mono.android.TypeManager.Activate ("Login.Adapter.AdapterEmployeeViewHolder, Login, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}

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
