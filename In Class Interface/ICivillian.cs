namespace In_Class_Interface
{
    public interface ICivillian
    {
         public string Name {get;set;}
         public int Gold {get;set;}

         void Gift(ICombatant friendly); 

    }
}