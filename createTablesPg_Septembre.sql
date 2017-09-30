
--Script de creation de la BD et des tables appropriées.


-- CREATE DE LA TABLE CODE_DOM

CREATE TABLE CODE_DOM(
	code VARCHAR(30) NOT NULL PRIMARY KEY,   
	descrip VARCHAR(400),
	util VARCHAR(30),
	actif char(1) NOT NULL,
	dt_cretn TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	dt_modf	TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL
);
update CODE_DOM set actif = null;
ALTER TABLE CODE_DOM ALTER COLUMN actif TYPE BOOLEAN NOT NULL;

ALTER TABLE CODE_DOM ALTER COLUMN actif SET DEFAULT TRUE;



-- CREATE DE LA TABLE VAL_DOM
CREATE TABLE VAL_DOM(
	id  SERIAL PRIMARY KEY,    
	code_dom VARCHAR(30),
	code_val VARCHAR(30),
	val VARCHAR(100),
	descrip VARCHAR(400),
	actif char(1),
	util VARCHAR(30),
	dt_cretn TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	dt_modf	TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
	CONSTRAINT code_dom_val_dom_fk FOREIGN KEY (code_dom) REFERENCES code_dom(code)
);



-- Valeurs
-- Vrai= (TRUE  ;'t' ;'true', 'y', 'yes', 'on', '1')
-- ==
-- Faux = (FALSE, 'f', 'false', 'n', 'no', 'off', '0')

-- CREATE DE LA TABLE LOCLSN
CREATE TABLE LOCLSN (
		id  SERIAL PRIMARY KEY, 
		num_civc varchar(30) , 
		voie varchar(100), 
		lot varchar(30) , 
		nom_rue varchar(100),
		tronc_Rue varchar(100),
		nom_cours_eau varchar(100),
		sectn_cours_eau varchar(100), 
		emplcmt varchar(200), 
		ville varchar(100) ,
		prov varchar(100) , 
		pays varchar(100) , 
		code_post varchar(10) , 
		Suprfc decimal(10,2), 
		lattd decimal(15,8),
		longtd decimal(15,8),
		util varchar(30), 
		dt_cretn TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 
		dt_modf TIMESTAMP DEFAULT CURRENT_TIMESTAMP
 );


-- CREATION DE LA TABLE ORGANISATION
CREATE TABLE ORG (
		  id  SERIAL PRIMARY KEY,
		  niv		       		    int,		  
		  nom						varchar(100)	,
		  sigle						varchar(100),
		  parnt						int,
		  cntct						varchar(120)	,
		  adr						int	,
		  util						varchar(30),
		  dt_cretn TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 
		  dt_modf TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
		  FOREIGN KEY (parnt) REFERENCES ORG(id),
		  FOREIGN KEY (adr) REFERENCES LOCLSN(id)
		) ;

-- CREATION DE LA TABLE PROFIL_UTIL
CREATE TABLE PROF_UTIL (
		  id  SERIAL PRIMARY KEY,
		  id_util       		varchar(60),
		  typ_util				varchar(60),
		  typ_propr				varchar(60)	,
		  nom					varchar(60)	,
		  pren		       		varchar(60),
		  org					int,
		  ss_org				int,
		  Telph					varchar(60),	
		  extnsn		       	varchar(60)	,
		  tel_cell			  	varchar(60)	,
		  id_local		  	    int,   -- FK vers localisation ou adresse
		  util					varchar(30),
		  dt_cretn TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 
		  dt_modf TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
		  --FOREIGN KEY (id_util) REFERENCES AspNetUsers(UserId), 
		  FOREIGN KEY (org) REFERENCES ORG(id),
		  FOREIGN KEY (ss_org) REFERENCES ORG(id),
		  FOREIGN KEY (id_local) REFERENCES LOCLSN(id)
		) ;


-- CREATION DE LA TABLE DEM_EVAL

CREATE TABLE DEM_EVAL (
		  id  SERIAL PRIMARY KEY,
		  id_proprio		      int,		  
		  descr					varchar(400)	,
		  st_demnd				varchar(30)		,		 
		  util						varchar(30),
		  dt_cretn TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 
		  dt_modf TIMESTAMP DEFAULT CURRENT_TIMESTAMP
		) ;

-- CREATION DE LA TABLE INTERVENTION
CREATE TABLE INTRV (
		 id  SERIAL PRIMARY KEY,
		 id_demnd		     int,
		 id_local		  	 int,		 
		 st_intrv			 varchar(30)	,
		 nb_arbr			 int	,
		 assgn_a		        	varchar(30)	,
		 util						varchar(30),
		 dt_cretn TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 
		 dt_modf TIMESTAMP DEFAULT CURRENT_TIMESTAMP
		) ;


-- CREATION DE LA TABLE MUN_COD_POST
CREATE TABLE MUN_COD_POST (
		  ID  SERIAL PRIMARY KEY,
		  cod_post		       	 varchar(20)	,		  
		  cod_mun					varchar(20)	,
		  nom_mun					varchar(100),
		  cod_reg		       		varchar(20)	,
		  nom_reg		       		varchar(100)		  
		) ;

-- CREATION DE LA TABLE MUN_REG_ADM
/*
CREATE TABLE MUN_REG_ADM (
		  ID  SERIAL PRIMARY KEY,
		  cod_reg		       		varchar(20)	,
		  nom_reg		       		varchar(100),
		  cod_mun					varchar(20)	,
		  nom_mun					varchar(100)	          
		) ;


--drop table arbre;

/*  Entête arbre (profil)	*/	  
CREATE TABLE arbre (
      id  SERIAL 	PRIMARY KEY,
	  num_arbre  	varchar(100),	  
      id_profil		int  ,	 	-- FK vers le demandeur/propriétaire
	  id_local		int  ,	   	-- FK vers localisation ou adresse
	  typ_emplcmt 	varchar(100),
	  orientatn		varchar(100),         -- Orientation (Ouest, est, Cour latérale droit, etc...)
	  ess 			varchar(100),	  
	  lattd 		numeric,
      longtd 		numeric,
	  dt_plant		TIMESTAMP,
	  type_Lieu 	varchar(100),
	  typ_abr		varchar(100),
	  typ_prop		varchar(100),
	  nom_topo		varchar(100),
	  util			varchar(30),
	  dt_cretn 		TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 
	  dt_modf 		TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	  FOREIGN KEY (id_profil) REFERENCES PROF_UTIL(id),	 	
	  FOREIGN KEY (id_local) REFERENCES LOCLSN(id)
	);
	
	CREATE TABLE eval_abr  (
      id  SERIAL PRIMARY KEY,
	  id_arbre  integer not null ,
	  id_evalteur	integer , 	
	  dt_eval TIMESTAMP ,
      dhp_tot 	numeric,
	  clas_haut  varchar(100),
	  intfrce 	varchar(100),
	  racdmt 	varchar(100),
	  acesbt_manu  varchar(100),
	  acesbt_machn varchar(100),
	  acesbt_cam  varchar(100),
	  contrnt_transp   varchar(100),
	  obstcl_sol  varchar(100),
	  constrct  varchar(100),
	  infrstr_urbn  varchar(100),
	  typ_abatg  varchar(100),	 
	  nb_tronc integer,
	  branch_maitr boolean,
	  action		varchar(400),	
	  concl		varchar(400),
	  util			varchar(30),	
	  dt_cretn TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 
	  dt_modf TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	  FOREIGN KEY (id_evalteur) REFERENCES PROF_UTIL(id),	 	
	  FOREIGN KEY (id_arbre) REFERENCES arbre (id)
	);

drop table Tronc;
/*  Tronc arbre ()	*/			
CREATE TABLE tronc (
		id  SERIAL PRIMARY KEY,
		id_eval  integer not null ,   		
		no_tronc	integer not null ,	
		id_tronc_parnt	integer REFERENCES tronc (id)	 ,   
		dhp	numeric,		
		diam_moy varchar(30),
		haut_moy varchar(30),
		long_moy	varchar(30), 
		long_valo	varchar(30), 
		morphlg varchar(100),	
		racdmt 	varchar(100),				
		cavt varchar(100), 
		fent_fissre varchar(100),
		blesr 	varchar(100),
		contaminatn	varchar(100),
		sympt_visuel	varchar(100), 		
		possede_cime boolean,			
		qual 		varchar(100),
		action		varchar(100),	
		concl		varchar(100),
		est_branch_maitr boolean,
		catgr_branch_maitr	varchar(100),  				
		nb_branch_maitr integer,		
		util	varchar(30),
		dt_cretn TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 
	    dt_modf TIMESTAMP DEFAULT CURRENT_TIMESTAMP ,
	    FOREIGN KEY (id_eval) REFERENCES eval_abr (id)	
		 );
		 
		 
CREATE TABLE cime (
		 id  SERIAL PRIMARY KEY,
		 id_tronc	integer not null  REFERENCES tronc (id), 		 
		 racdmt 	varchar(100),
		 dens_brach	varchar(100), 
		 dens_feuille	varchar(100), 
		 dens_rameaux	varchar(100), 
		 sympt_visuel	varchar(100), 
		 deefaut			varchar(100), 
		 intfrce	varchar(100), 
		 travaux	varchar(100)	,	
		 util						varchar(30),
	     dt_cretn TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 
	     dt_modf TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	      FOREIGN KEY (id_tronc) REFERENCES tronc (id)  	
		 );
		 
	
CREATE TABLE souche (
		id  SERIAL PRIMARY KEY,
		id_eval  			integer not null,
		dhs 				numeric,
		acces_nutrmt		varchar(100), 
		aeration_Sol		varchar(100), 
		surf_deplmt_racin	varchar(100), 
		racine_hs			boolean,
		blesre_racine		varchar(100), 
		cavite_hrs_Sol		boolean, 
		exig_essouchmt		varchar(100), 
		typ_essouchmt		varchar(100), 
		profdeur_essouchmt	numeric, 
		ray_rognage 		numeric, 
		defaut				varchar(100), 		
		infrstr				varchar(100), 
		specificite			varchar(100),
		haut_souche			numeric,
		exig_abat			varchar(30), 
		espace_subs			boolean, 
		fosse_plant			boolean, 				
		util				varchar(30) not null,
		dt_cretn TIMESTAMP DEFAULT CURRENT_TIMESTAMP not null,
	    dt_modf TIMESTAMP DEFAULT CURRENT_TIMESTAMP not null,
	      FOREIGN KEY (id_eval) REFERENCES eval_abr (id)		
		 );


CREATE TABLE photo_abr(
	id  SERIAL  NOT NULL,
	id_arbre  integer not null , 
	photo OID,
 CONSTRAINT Pk_arbrephoto PRIMARY KEY  (id ),
  FOREIGN KEY (id_arbre) REFERENCES ARBRE (id)	);

  