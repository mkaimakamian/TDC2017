using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewLayer
{
    public class Codes
    { 
        public const string GE001 = "GE001"; //Herramientas
            public const string OP001 = "OP001"; //Cambiar idioma
            public const string GE002 = "GE002"; //Seguridad
                public const string GE003 = "GE003"; //Usuario
                    //public const string OP002 = "OP002"; //Listar usuario --> opcion por default grilla
                    public const string OP003 = "OP003"; //Crear usuario
                    public const string OP004 = "OP004"; //Editar usuario
                    public const string OP005 = "OP005"; //Eliminar usuario               
            public const string GE004 = "GE004"; //Perfiles
                //public const string OP006 = "OP006"; //Crear usuario
                public const string OP007 = "OP007"; //Crear perfil
                public const string OP008 = "OP008"; //Editar perfil
                public const string OP009 = "OP009"; //Eliminar perfil

            public const string GE005 = "GE005"; //Mantenimiento
                public const string OP010 = "OP010"; // Listar bitácora
                public const string GE006 = "GE006"; //Respaldo
                    public const string OP011 = "OP011"; // Copia de respaldo
                    public const string OP012 = "OP012"; // Cargar copia de respaldo
            public const string GE007 = "GE007"; //Integridad
                //public const string OP013 = "OP013"; // Check horizontal --> Sólo existe una opción, por lo que se considera el GE007 nada más
                //public const string OP014 = "OP014"; // Check vertical
        
        public const string GE008 = "GE008"; //Personas
            public const string GE009 = "GE009"; //Donadores
                public const string OP014 = "OP014"; // Crear donador
                public const string OP015 = "OP015"; // Editar donador
                public const string OP016 = "OP016"; // Eliminar donador
            public const string GE014 = "GE014"; //Beneficiarios
                public const string OP030 = "OP030"; // Crear beneficiario
                public const string OP031 = "OP031"; // Editar beneficiario
                public const string OP032 = "OP032"; // Eliminar beneficiario

            public const string GE010 = "GE010"; //Stock
                public const string GE011 = "GE011"; //Donaciones
                    public const string OP018 = "OP018"; // Crear donacion
                    public const string OP019 = "OP019"; // Editar donacion
                    public const string OP020 = "OP020"; // Eliminar donacion
                public const string GE012 = "GE012"; //Tipos artículos
                    public const string OP022 = "OP022"; // Crear Tipos artículos
                    public const string OP023 = "OP023"; // Editar Tipos artículos
                    public const string OP024 = "OP024"; // Eliminar Tipos artículos
                public const string GE013 = "GE013"; //Stock (gestión)
                    public const string OP026 = "OP026"; // Crear stock
                    public const string OP027 = "OP027"; // Editar stock
                    public const string OP028 = "OP028"; // Eliminar stock
                  

        
        public const string GE015 = "GE015"; //Logística
            public const string GE016 = "GE016"; //Órdenes de salida
                public const string OP034 = "OP034"; // Crear Órdenes de salida
                public const string OP035 = "OP035"; // Editar Órdenes de salida
                public const string OP036 = "OP036"; // Eliminar Órdenes de salida
        
        

        //:::TRADUCCIONES:::
        public const string BTN_ACCEPT = "BTN_ACCEPT"; //Botones comunes
        public const string BTN_CANCEL = "BTN_CANCEL";
        public const string BTN_NEW = "BTN_NEW";
        public const string BTN_CLOSE = "BTN_CLOSE";
        public const string BTN_EDIT = "BTN_EDIT";
        public const string BTN_DELETE = "BTN_DELETE";

        public const string LBL_ACTIVE = "LBL_ACTIVE";
        public const string LBL_LANGUAGE = "LBL_LANGUAGE";
        public const string LBL_NAME = "LBL_NAME";
        public const string LBL_LAST_NAME = "LBL_LAST_NAME";
        public const string LBL_BIRTHDAY = "LBL_BIRTHDAY";
        public const string LBL_EMAIL = "LBL_EMAIL";
        public const string LBL_PHONE = "LBL_PHONE";
        public const string LBL_MALE = "LBL_MALE";
        public const string LBL_FEMALE = "LBL_FEMALE";
        public const string LBL_UID = "LBL_UID";
        public const string LBL_OBSERVATION = "LBL_OBSERVATION";

        public const string LBL_STREET = "LBL_STREET";
        public const string LBL_NUMBER = "LBL_NUMBER";
        public const string LBL_APARTMENT = "LBL_APARTMENT";
        public const string LBL_COUNTRY = "LBL_COUNTRY";

        public const string LBL_COMPANY = "LBL_COMPANY";
        public const string LBL_CATEGORY = "LBL_CATEGORY";

        public const string LBL_CAN_CONTACT = "LBL_CAN_CONTACT";

        public const string LBL_ARRIVAL = "LBL_ARRIVAL";
        public const string LBL_RESPONSIBLE = "LBL_RESPONSIBLE";
        public const string LBL_DONOR = "LBL_DONOR";
        public const string LBL_ITEMS = "LBL_ITEMS";
        public const string LBL_LOT = "LBL_LOT";
        public const string LBL_CONTACT_INFO = "LBL_CONTACT_INFO";
        public const string LBL_PICKUP = "LBL_PICKUP";
        
        public const string LBL_PASSWORD = "LBL_PASSWORD";
        public const string LBL_PASSWORD_CHECK = "LBL_PASSWORD_CHECK";
        public const string LBL_PROFILE = "LBL_PROFILE";
        public const string LBL_DESCRIPTION = "LBL_DESCRIPTION";
        public const string LBL_PERMISSIONS = "LBL_PERMISSIONS";

        public const string LBL_EDIBLE = "LBL_EDIBLE";
        public const string LBL_MEDICINE = "LBL_MEDICINE";
        public const string LBL_INDUMENTARY = "LBL_INDUMENTARY";
        public const string LBL_CONSTRUCTION = "LBL_CONSTRUCTION";
        public const string LBL_OTHER = "LBL_OTHER";
        public const string LBL_PERISHABLE = "LBL_PERISHABLE";

        public const string LBL_DEPOT = "LBL_DEPOT";
        public const string LBL_ITEM_TYPE = "LBL_ITEM_TYPE";
        public const string LBL_QUANTITY = "LBL_QUANTITY";
        public const string LBL_DUEDATE = "LBL_DUEDATE";
        public const string LBL_LOCATION = "LBL_LOCATION";

        public const string LBL_DESTINATARY = "LBL_DESTINATARY";
        public const string LBL_AGE_RANGE = "LBL_AGE_RANGE";
        public const string LBL_ACCESSIBILITY = "LBL_ACCESSIBILITY";
        public const string LBL_SALUBRITY = "LBL_SALUBRITY";
        public const string LBL_MAJOR_PROBLEM = "LBL_MAJOR_PROBLEM";

        public const string LBL_DESC_DESTINATARY = "LBL_DESC_DESTINATARY";
        public const string LBL_DESC_AGE_RANGE = "LBL_DESC_AGE_RANGE";
        public const string LBL_DESC_SALUBRITY = "LBL_DESC_SALUBRITY";
        public const string LBL_DESC_ACCESSIBILITY = "LBL_DESC_ACCESSIBILITY";
        public const string LBL_DESC_MAJOR_PROBLEM = "LBL_DESC_MAJOR_PROBLEM";

        public const string LBL_BENEFICIARY = "LBL_BENEFICIARY";
        public const string LBL_STOCK = "LBL_STOCK";
        
        public const string MNU_GE001 = "MNU_GE001"; //Menú herramientas
            public const string MNU_OP001 = "MNU_OP001"; //Cambiar idioma
                public const string MNU_OP001_LBL_LANGUAGE = "MNU_OP001_LBL_LANGUAGE"; //Frase de cambiar idioma
        
        public const string MNU_GE002 = "MNU_GE002"; //Seguridad
            public const string MNU_GE003 = "MNU_GE003"; //Usuario
            public const string MNU_GE004 = "MNU_GE004"; //Perfiles
        
        public const string MNU_GE005 = "MNU_GE005"; //Mantenimiento
            public const string MNU_OP010 = "MNU_OP010"; //Bitacora
            public const string MNU_GE006 = "MNU_GE006"; //Respaldo
                public const string BTN_BACKUP = "BTN_BACKUP"; //Crear respaldo
                public const string BTN_RESTORE = "BTN_RESTORE"; //Cargar respaldo
            public const string MNU_GE007 = "MNU_GE007"; //Integridad
                public const string MNU_GE007_LBL_INTEGRITY = "MNU_GE007_LBL_INTEGRITY";
                public const string BTN_CHECK_INTEGRITY = "BTN_CHECK_INTEGRITY";

        public const string MNU_GE008 = "MNU_GE008"; //Personas   
            public const string MNU_GE009 = "MNU_GE009"; //Donadores
            public const string MNU_GE014 = "MNU_GE014"; //Beneficiarios

        public const string MNU_GE010 = "MNU_GE010"; //Stock
            public const string MNU_GE011 = "MNU_GE011"; //Donaciones
            public const string MNU_GE012 = "MNU_GE012"; //tipo de artículos
            public const string MNU_GE013 = "MNU_GE013"; //Stock (gestión)

       
        public const string MNU_GE015 = "MNU_GE015"; //Logística
            public const string MNU_GE016 = "MNU_GE016"; //Órdenes de salida        

            public const string OK_OPERATION = "OK_OPERATION";
            public const string SAVING_ERROR = "SAVING_ERROR";
            public const string UPDATING_ERROR = "UPDATING_ERROR";
            public const string DELETING_ERROR = "DELETING_ERROR";
            public const string RETRIEVING_ERROR = "RETRIEVING_ERROR";
            public const string PROPERTY_ERROR = "PROPERTY_ERROR"; // cuando no se puede guardar una propiedad o algo
            public const string SAVE_CHANGES_QUESTION = "SAVE_CHANGES_QUESTION";
    }
}
