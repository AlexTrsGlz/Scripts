
//Propiedad de Acceso lectura y esctirura
private ClaseGenerica _objClaseGenerica;
protected ClaseGenerica ObjClaseGenerica
{
    get
    {
        if (_objClaseGenerica == null)
        { _objClaseGenerica = new ClaseGenerica(); }
        return _objClaseGenerica;
    }
    set
    { _objClaseGenerica = value; }
}


//Propiedad de Acceso de solo lectura para un LoginInfo
private LoginInfo _li;
protected LoginInfo Li
{
    get 
    {
        if (_li == null)
        { _li = getLoginInfo(); }
        return _li; 
    }
}