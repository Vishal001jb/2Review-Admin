package md5dadab86d543333d749372a60ac649408;


public class EmployeeDetail_RecyclerAdapter_MyView
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Login.Activity.EmployeeDetail+RecyclerAdapter+MyView, Login, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", EmployeeDetail_RecyclerAdapter_MyView.class, __md_methods);
	}


	public EmployeeDetail_RecyclerAdapter_MyView (android.view.View p0)
	{
		super (p0);
		if (getClass () == EmployeeDetail_RecyclerAdapter_MyView.class)
			mono.android.TypeManager.Activate ("Login.Activity.EmployeeDetail+RecyclerAdapter+MyView, Login, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.View, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
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
