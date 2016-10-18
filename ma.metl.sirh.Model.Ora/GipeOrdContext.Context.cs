﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ma.metl.sirh.Model.Ora
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GipeOrdContext : DbContext
    {
        public GipeOrdContext()
            : base("name=GipeOrdContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ACTE_EVENT_HIST> ACTE_EVENT_HIST { get; set; }
        public virtual DbSet<EVT_CARRIER_CONFIGURATION> EVT_CARRIER_CONFIGURATION { get; set; }
        public virtual DbSet<SMACTUALPARAMETER_S> SMACTUALPARAMETER_S { get; set; }
        public virtual DbSet<SMAGENTJOB_S> SMAGENTJOB_S { get; set; }
        public virtual DbSet<SMARCHIVE_S> SMARCHIVE_S { get; set; }
        public virtual DbSet<SMBREAKABLELINK> SMBREAKABLELINKS { get; set; }
        public virtual DbSet<SMCONFIGURATION> SMCONFIGURATIONs { get; set; }
        public virtual DbSet<SMCONSOLESOSETTING_S> SMCONSOLESOSETTING_S { get; set; }
        public virtual DbSet<SMDATABASE_S> SMDATABASE_S { get; set; }
        public virtual DbSet<SMDBAUTH_S> SMDBAUTH_S { get; set; }
        public virtual DbSet<SMDEFAUTH_S> SMDEFAUTH_S { get; set; }
        public virtual DbSet<SMDEPENDENTLINK> SMDEPENDENTLINKS { get; set; }
        public virtual DbSet<SMDISTRIBUTIONSET_S> SMDISTRIBUTIONSET_S { get; set; }
        public virtual DbSet<SMFOLDER_S> SMFOLDER_S { get; set; }
        public virtual DbSet<SMFORMALPARAMETER_S> SMFORMALPARAMETER_S { get; set; }
        public virtual DbSet<SMGLOBALCONFIGURATION_S> SMGLOBALCONFIGURATION_S { get; set; }
        public virtual DbSet<SMHOST_S> SMHOST_S { get; set; }
        public virtual DbSet<SMHOSTAUTH_S> SMHOSTAUTH_S { get; set; }
        public virtual DbSet<SMINSTALLATION_S> SMINSTALLATION_S { get; set; }
        public virtual DbSet<SMLOGMESSAGE_S> SMLOGMESSAGE_S { get; set; }
        public virtual DbSet<SMMONTHLYENTRY_S> SMMONTHLYENTRY_S { get; set; }
        public virtual DbSet<SMMONTHWEEKENTRY_S> SMMONTHWEEKENTRY_S { get; set; }
        public virtual DbSet<SMMOWNERLINK> SMMOWNERLINKS { get; set; }
        public virtual DbSet<SMOMSTRING_S> SMOMSTRING_S { get; set; }
        public virtual DbSet<SMOWNERLINK> SMOWNERLINKS { get; set; }
        public virtual DbSet<SMP_AD_ADDRESSES_> SMP_AD_ADDRESSES_ { get; set; }
        public virtual DbSet<SMP_AD_DISCOVERED_NODES_> SMP_AD_DISCOVERED_NODES_ { get; set; }
        public virtual DbSet<SMP_AD_NODES_> SMP_AD_NODES_ { get; set; }
        public virtual DbSet<SMP_AD_PARMS_> SMP_AD_PARMS_ { get; set; }
        public virtual DbSet<SMP_AUTO_DISCOVERY_ITEM_> SMP_AUTO_DISCOVERY_ITEM_ { get; set; }
        public virtual DbSet<SMP_AUTO_DISCOVERY_PARMS_> SMP_AUTO_DISCOVERY_PARMS_ { get; set; }
        public virtual DbSet<SMP_BLOB_> SMP_BLOB_ { get; set; }
        public virtual DbSet<SMP_JOB_> SMP_JOB_ { get; set; }
        public virtual DbSet<SMP_SERVICE_DATA_> SMP_SERVICE_DATA_ { get; set; }
        public virtual DbSet<SMP_SERVICE_GROUP_DEFN_> SMP_SERVICE_GROUP_DEFN_ { get; set; }
        public virtual DbSet<SMP_SERVICE_GROUP_ITEM_> SMP_SERVICE_GROUP_ITEM_ { get; set; }
        public virtual DbSet<SMP_SERVICE_ITEM_> SMP_SERVICE_ITEM_ { get; set; }
        public virtual DbSet<SMP_UPDATESERVICES_CALLED_> SMP_UPDATESERVICES_CALLED_ { get; set; }
        public virtual DbSet<SMPACKAGE_S> SMPACKAGE_S { get; set; }
        public virtual DbSet<SMPARALLELJOB_S> SMPARALLELJOB_S { get; set; }
        public virtual DbSet<SMPARALLELOPERATION_S> SMPARALLELOPERATION_S { get; set; }
        public virtual DbSet<SMPARALLELSTATEMENT_S> SMPARALLELSTATEMENT_S { get; set; }
        public virtual DbSet<SMPRODUCT_S> SMPRODUCT_S { get; set; }
        public virtual DbSet<SMPRODUCTATTRIBUTE_S> SMPRODUCTATTRIBUTE_S { get; set; }
        public virtual DbSet<SMRELEASE_S> SMRELEASE_S { get; set; }
        public virtual DbSet<SMRUN_S> SMRUN_S { get; set; }
        public virtual DbSet<SMSCHEDULE_S> SMSCHEDULE_S { get; set; }
        public virtual DbSet<SMSHAREDORACLECLIENT_S> SMSHAREDORACLECLIENT_S { get; set; }
        public virtual DbSet<SMSHAREDORACLECONFIGURATION_S> SMSHAREDORACLECONFIGURATION_S { get; set; }
        public virtual DbSet<SMTABLESPACE_S> SMTABLESPACE_S { get; set; }
        public virtual DbSet<SMVCENDPOINT_S> SMVCENDPOINT_S { get; set; }
        public virtual DbSet<SMWEEKLYENTRY_S> SMWEEKLYENTRY_S { get; set; }
        public virtual DbSet<ZLOG_TAB_AG_PROMO> ZLOG_TAB_AG_PROMO { get; set; }
        public virtual DbSet<ZLOG_TAB_ANIMATEUR> ZLOG_TAB_ANIMATEUR { get; set; }
        public virtual DbSet<ZLOG_TAB_ART> ZLOG_TAB_ART { get; set; }
        public virtual DbSet<ZLOG_TAB_ART_OLD> ZLOG_TAB_ART_OLD { get; set; }
        public virtual DbSet<ZLOG_TAB_CADRE> ZLOG_TAB_CADRE { get; set; }
        public virtual DbSet<ZLOG_TAB_CADRE_OLD> ZLOG_TAB_CADRE_OLD { get; set; }
        public virtual DbSet<ZLOG_TAB_CATEG> ZLOG_TAB_CATEG { get; set; }
        public virtual DbSet<ZLOG_TAB_CATEG_OLD> ZLOG_TAB_CATEG_OLD { get; set; }
        public virtual DbSet<ZLOG_TAB_CHAMPS> ZLOG_TAB_CHAMPS { get; set; }
        public virtual DbSet<ZLOG_TAB_CONSIDERANTS> ZLOG_TAB_CONSIDERANTS { get; set; }
        public virtual DbSet<ZLOG_TAB_CORPS> ZLOG_TAB_CORPS { get; set; }
        public virtual DbSet<ZLOG_TAB_CORPS_OLD> ZLOG_TAB_CORPS_OLD { get; set; }
        public virtual DbSet<ZLOG_TAB_DECORATIONS> ZLOG_TAB_DECORATIONS { get; set; }
        public virtual DbSet<ZLOG_TAB_DEP> ZLOG_TAB_DEP { get; set; }
        public virtual DbSet<ZLOG_TAB_E_E_I_D> ZLOG_TAB_E_E_I_D { get; set; }
        public virtual DbSet<ZLOG_TAB_E_E_I_D_OLD> ZLOG_TAB_E_E_I_D_OLD { get; set; }
        public virtual DbSet<ZLOG_TAB_ECHELLE> ZLOG_TAB_ECHELLE { get; set; }
        public virtual DbSet<ZLOG_TAB_ECHELON> ZLOG_TAB_ECHELON { get; set; }
        public virtual DbSet<ZLOG_TAB_ENREG> ZLOG_TAB_ENREG { get; set; }
        public virtual DbSet<ZLOG_TAB_ENREG_COMMUNICATION> ZLOG_TAB_ENREG_COMMUNICATION { get; set; }
        public virtual DbSet<ZLOG_TAB_ENTSIGL> ZLOG_TAB_ENTSIGL { get; set; }
        public virtual DbSet<ZLOG_TAB_ERR> ZLOG_TAB_ERR { get; set; }
        public virtual DbSet<ZLOG_TAB_ERR_PB> ZLOG_TAB_ERR_PB { get; set; }
        public virtual DbSet<ZLOG_TAB_ERREUR> ZLOG_TAB_ERREUR { get; set; }
        public virtual DbSet<ZLOG_TAB_ERREUR_PB> ZLOG_TAB_ERREUR_PB { get; set; }
        public virtual DbSet<ZLOG_TAB_EXERCICE> ZLOG_TAB_EXERCICE { get; set; }
        public virtual DbSet<ZLOG_TAB_FCT> ZLOG_TAB_FCT { get; set; }
        public virtual DbSet<ZLOG_TAB_G_E_I_D> ZLOG_TAB_G_E_I_D { get; set; }
        public virtual DbSet<ZLOG_TAB_G_E_I_D_OLD> ZLOG_TAB_G_E_I_D_OLD { get; set; }
        public virtual DbSet<ZLOG_TAB_GARDE> ZLOG_TAB_GARDE { get; set; }
        public virtual DbSet<ZLOG_TAB_GRADE> ZLOG_TAB_GRADE { get; set; }
        public virtual DbSet<ZLOG_TAB_GRADE_OLD> ZLOG_TAB_GRADE_OLD { get; set; }
        public virtual DbSet<ZLOG_TAB_GRSIGL> ZLOG_TAB_GRSIGL { get; set; }
        public virtual DbSet<ZLOG_TAB_IMP_BUD> ZLOG_TAB_IMP_BUD { get; set; }
        public virtual DbSet<ZLOG_TAB_LANGUE> ZLOG_TAB_LANGUE { get; set; }
        public virtual DbSet<ZLOG_TAB_LIEN_PAR> ZLOG_TAB_LIEN_PAR { get; set; }
        public virtual DbSet<ZLOG_TAB_LOC> ZLOG_TAB_LOC { get; set; }
        public virtual DbSet<ZLOG_TAB_MESSAGE> ZLOG_TAB_MESSAGE { get; set; }
        public virtual DbSet<ZLOG_TAB_MOD_AV_GRADE> ZLOG_TAB_MOD_AV_GRADE { get; set; }
        public virtual DbSet<ZLOG_TAB_MOUVEMENT_DS> ZLOG_TAB_MOUVEMENT_DS { get; set; }
        public virtual DbSet<ZLOG_TAB_NAT> ZLOG_TAB_NAT { get; set; }
        public virtual DbSet<ZLOG_TAB_NIVEAU_INST> ZLOG_TAB_NIVEAU_INST { get; set; }
        public virtual DbSet<ZLOG_TAB_ORG_DET> ZLOG_TAB_ORG_DET { get; set; }
        public virtual DbSet<ZLOG_TAB_ORGANISATEUR> ZLOG_TAB_ORGANISATEUR { get; set; }
        public virtual DbSet<ZLOG_TAB_PARAMETRES> ZLOG_TAB_PARAMETRES { get; set; }
        public virtual DbSet<ZLOG_TAB_PAYS> ZLOG_TAB_PAYS { get; set; }
        public virtual DbSet<ZLOG_TAB_PB_DS> ZLOG_TAB_PB_DS { get; set; }
        public virtual DbSet<ZLOG_TAB_POS> ZLOG_TAB_POS { get; set; }
        public virtual DbSet<ZLOG_TAB_PRF_CONJ> ZLOG_TAB_PRF_CONJ { get; set; }
        public virtual DbSet<ZLOG_TAB_PROFILES> ZLOG_TAB_PROFILES { get; set; }
        public virtual DbSet<ZLOG_TAB_REP_SAL> ZLOG_TAB_REP_SAL { get; set; }
        public virtual DbSet<ZLOG_TAB_ROLES> ZLOG_TAB_ROLES { get; set; }
        public virtual DbSet<ZLOG_TAB_RUBRIQUES> ZLOG_TAB_RUBRIQUES { get; set; }
        public virtual DbSet<ZLOG_TAB_S_ORD> ZLOG_TAB_S_ORD { get; set; }
        public virtual DbSet<ZLOG_TAB_S_ST> ZLOG_TAB_S_ST { get; set; }
        public virtual DbSet<ZLOG_TAB_SAF> ZLOG_TAB_SAF { get; set; }
        public virtual DbSet<ZLOG_TAB_SAF_FTF> ZLOG_TAB_SAF_FTF { get; set; }
        public virtual DbSet<ZLOG_TAB_SAF_FTF_OLD> ZLOG_TAB_SAF_FTF_OLD { get; set; }
        public virtual DbSet<ZLOG_TAB_SAF_LOC> ZLOG_TAB_SAF_LOC { get; set; }
        public virtual DbSet<ZLOG_TAB_SAF_OLD> ZLOG_TAB_SAF_OLD { get; set; }
        public virtual DbSet<ZLOG_TAB_SEQUENCE_SIGL> ZLOG_TAB_SEQUENCE_SIGL { get; set; }
        public virtual DbSet<ZLOG_TAB_SEQUENCES> ZLOG_TAB_SEQUENCES { get; set; }
        public virtual DbSet<ZLOG_TAB_SIT_CONJ> ZLOG_TAB_SIT_CONJ { get; set; }
        public virtual DbSet<ZLOG_TAB_SIT_F_AG> ZLOG_TAB_SIT_F_AG { get; set; }
        public virtual DbSet<ZLOG_TAB_SIT_PAC> ZLOG_TAB_SIT_PAC { get; set; }
        public virtual DbSet<ZLOG_TAB_SPECIALITE> ZLOG_TAB_SPECIALITE { get; set; }
        public virtual DbSet<ZLOG_TAB_SPECIALITE_FORMATION> ZLOG_TAB_SPECIALITE_FORMATION { get; set; }
        public virtual DbSet<ZLOG_TAB_SRV> ZLOG_TAB_SRV { get; set; }
        public virtual DbSet<ZLOG_TAB_SRV_VAL> ZLOG_TAB_SRV_VAL { get; set; }
        public virtual DbSet<ZLOG_TAB_SS_TYP_MVT> ZLOG_TAB_SS_TYP_MVT { get; set; }
        public virtual DbSet<ZLOG_TAB_TACHES> ZLOG_TAB_TACHES { get; set; }
        public virtual DbSet<ZLOG_TAB_TEMP> ZLOG_TAB_TEMP { get; set; }
        public virtual DbSet<ZLOG_TAB_TYP_MENTION> ZLOG_TAB_TYP_MENTION { get; set; }
        public virtual DbSet<ZLOG_TAB_TYP_MVT> ZLOG_TAB_TYP_MVT { get; set; }
        public virtual DbSet<ZLOG_TAB_TYP_MVT_LC> ZLOG_TAB_TYP_MVT_LC { get; set; }
        public virtual DbSet<ZLOG_TAB_TYPE_FORMATION> ZLOG_TAB_TYPE_FORMATION { get; set; }
        public virtual DbSet<ZLOG_TAB_TYPES_CONGES> ZLOG_TAB_TYPES_CONGES { get; set; }
        public virtual DbSet<ZLOG_TAB_VERSIONS_BASE> ZLOG_TAB_VERSIONS_BASE { get; set; }
        public virtual DbSet<ZLOG_TABLES_DS_MVT> ZLOG_TABLES_DS_MVT { get; set; }
        public virtual DbSet<ACTE> ACTEs { get; set; }
        public virtual DbSet<ACTE_HIST> ACTE_HIST { get; set; }
        public virtual DbSet<AG_PARAM> AG_PARAM { get; set; }
        public virtual DbSet<AGENT> AGENTs { get; set; }
        public virtual DbSet<AGENT_DECORE> AGENT_DECORE { get; set; }
        public virtual DbSet<AGENT_DS> AGENT_DS { get; set; }
        public virtual DbSet<AGENT_FORME> AGENT_FORME { get; set; }
        public virtual DbSet<AGENT_NOTE> AGENT_NOTE { get; set; }
        public virtual DbSet<AGENT_ORD> AGENT_ORD { get; set; }
        public virtual DbSet<AGENT_PB> AGENT_PB { get; set; }
        public virtual DbSet<AGENT_PB_DS> AGENT_PB_DS { get; set; }
        public virtual DbSet<AGENT_PROPOSE> AGENT_PROPOSE { get; set; }
        public virtual DbSet<AGENT_TV> AGENT_TV { get; set; }
        public virtual DbSet<AJOUT_CONSIDERANTS> AJOUT_CONSIDERANTS { get; set; }
        public virtual DbSet<BORDEREAU> BORDEREAUx { get; set; }
        public virtual DbSet<CAP> CAPs { get; set; }
        public virtual DbSet<CHAMPS_DS_MVT> CHAMPS_DS_MVT { get; set; }
        public virtual DbSet<COMPATIBILITE_AVEC_ANCIEN_GRAD> COMPATIBILITE_AVEC_ANCIEN_GRAD { get; set; }
        public virtual DbSet<CONDITION> CONDITIONS { get; set; }
        public virtual DbSet<CONGE> CONGES { get; set; }
        public virtual DbSet<CONJOINT> CONJOINTs { get; set; }
        public virtual DbSet<CONJOINT_DS> CONJOINT_DS { get; set; }
        public virtual DbSet<CONJOINT_ORD> CONJOINT_ORD { get; set; }
        public virtual DbSet<COUT_FORMATION> COUT_FORMATION { get; set; }
        public virtual DbSet<CRITERES_CAP> CRITERES_CAP { get; set; }
        public virtual DbSet<CTL_PROCEDURES> CTL_PROCEDURES { get; set; }
        public virtual DbSet<DECHARG_INDIV> DECHARG_INDIV { get; set; }
        public virtual DbSet<DIPL_INDEM> DIPL_INDEM { get; set; }
        public virtual DbSet<DIPLOME> DIPLOMEs { get; set; }
        public virtual DbSet<DIPLOME_OLD> DIPLOME_OLD { get; set; }
        public virtual DbSet<DIPLOME1> DIPLOMES1 { get; set; }
        public virtual DbSet<DIPLOMES_DS> DIPLOMES_DS { get; set; }
        public virtual DbSet<ENF_PAC> ENF_PAC { get; set; }
        public virtual DbSet<ENF_PAC_DS> ENF_PAC_DS { get; set; }
        public virtual DbSet<ENF_PAC_ORD> ENF_PAC_ORD { get; set; }
        public virtual DbSet<ERRATUM_POSTE> ERRATUM_POSTE { get; set; }
        public virtual DbSet<ETAT_ENGAGEMENT> ETAT_ENGAGEMENT { get; set; }
        public virtual DbSet<EXTRAC> EXTRACs { get; set; }
        public virtual DbSet<EXTRACT> EXTRACTs { get; set; }
        public virtual DbSet<EXTRACT_CHAMPS_PLUS> EXTRACT_CHAMPS_PLUS { get; set; }
        public virtual DbSet<INDEM_AG> INDEM_AG { get; set; }
        public virtual DbSet<INDEM_AG_DS> INDEM_AG_DS { get; set; }
        public virtual DbSet<INDEM_FONC> INDEM_FONC { get; set; }
        public virtual DbSet<INDEMNITE> INDEMNITEs { get; set; }
        public virtual DbSet<INHIBER_CTL_POSTES> INHIBER_CTL_POSTES { get; set; }
        public virtual DbSet<INHIBER_CTL_POSTES_ACTE> INHIBER_CTL_POSTES_ACTE { get; set; }
        public virtual DbSet<INIT_CHAMPS> INIT_CHAMPS { get; set; }
        public virtual DbSet<INT_SERV> INT_SERV { get; set; }
        public virtual DbSet<INT_SERV_DS> INT_SERV_DS { get; set; }
        public virtual DbSet<INT_SERV_ORD> INT_SERV_ORD { get; set; }
        public virtual DbSet<LIBELLE_CHAMPS_DS_MVT> LIBELLE_CHAMPS_DS_MVT { get; set; }
        public virtual DbSet<LOI_CADRE> LOI_CADRE { get; set; }
        public virtual DbSet<MENTION_POSTE> MENTION_POSTE { get; set; }
        public virtual DbSet<MENTION> MENTIONS { get; set; }
        public virtual DbSet<MOUVEMENTS_CONSID> MOUVEMENTS_CONSID { get; set; }
        public virtual DbSet<MVT_PB> MVT_PB { get; set; }
        public virtual DbSet<MVT_SIT_ADM> MVT_SIT_ADM { get; set; }
        public virtual DbSet<OLD_DEP_UTILISATEURS> OLD_DEP_UTILISATEURS { get; set; }
        public virtual DbSet<OLD_DGAC_UTILISATEURS> OLD_DGAC_UTILISATEURS { get; set; }
        public virtual DbSet<OLD_DMM_UTILISATEURS> OLD_DMM_UTILISATEURS { get; set; }
        public virtual DbSet<OLD_DPDPM_UTILISATEURS> OLD_DPDPM_UTILISATEURS { get; set; }
        public virtual DbSet<OLD_DR_UTILISATEURS> OLD_DR_UTILISATEURS { get; set; }
        public virtual DbSet<OLD_DRH_UTILISATEURS> OLD_DRH_UTILISATEURS { get; set; }
        public virtual DbSet<OLD_DTRSR_UTILISATEURS> OLD_DTRSR_UTILISATEURS { get; set; }
        public virtual DbSet<PAIE> PAIEs { get; set; }
        public virtual DbSet<PLAGE_POSTES> PLAGE_POSTES { get; set; }
        public virtual DbSet<PLAN_FORMATION_CONTINUE> PLAN_FORMATION_CONTINUE { get; set; }
        public virtual DbSet<PREOCCUPATION_PB> PREOCCUPATION_PB { get; set; }
        public virtual DbSet<PREP_EFFECTIF_MENT> PREP_EFFECTIF_MENT { get; set; }
        public virtual DbSet<PREP_ERRATUM> PREP_ERRATUM { get; set; }
        public virtual DbSet<PREP_ERRATUM_INFO> PREP_ERRATUM_INFO { get; set; }
        public virtual DbSet<PREP_LOI_CADRE> PREP_LOI_CADRE { get; set; }
        public virtual DbSet<PREP_MENTIONS> PREP_MENTIONS { get; set; }
        public virtual DbSet<PROFILES_USER> PROFILES_USER { get; set; }
        public virtual DbSet<PROMO_GRADE> PROMO_GRADE { get; set; }
        public virtual DbSet<RC_PARAM> RC_PARAM { get; set; }
        public virtual DbSet<RELIQUAT> RELIQUATS { get; set; }
        public virtual DbSet<ROLES_PROFILE> ROLES_PROFILE { get; set; }
        public virtual DbSet<RUBRIQUES_ETAT_ENGAGEMENT> RUBRIQUES_ETAT_ENGAGEMENT { get; set; }
        public virtual DbSet<RUBRIQUES_PAIE> RUBRIQUES_PAIE { get; set; }
        public virtual DbSet<SERV_ANT> SERV_ANT { get; set; }
        public virtual DbSet<SERV_ANT_DS> SERV_ANT_DS { get; set; }
        public virtual DbSet<SERV_ANT_ORD> SERV_ANT_ORD { get; set; }
        public virtual DbSet<SESSION_ANIME_PAR> SESSION_ANIME_PAR { get; set; }
        public virtual DbSet<SESSION_EVALUE> SESSION_EVALUE { get; set; }
        public virtual DbSet<SESSION_FORMATION> SESSION_FORMATION { get; set; }
        public virtual DbSet<SIT_ADM> SIT_ADM { get; set; }
        public virtual DbSet<SIT_ADM_DS> SIT_ADM_DS { get; set; }
        public virtual DbSet<SIT_ADM_ORD> SIT_ADM_ORD { get; set; }
        public virtual DbSet<SIT_PB> SIT_PB { get; set; }
        public virtual DbSet<SIT_PB_DS> SIT_PB_DS { get; set; }
        public virtual DbSet<SOUS_ORD_PB> SOUS_ORD_PB { get; set; }
        public virtual DbSet<SOUS_ORD_USER> SOUS_ORD_USER { get; set; }
        public virtual DbSet<TAB_AG_PROMO> TAB_AG_PROMO { get; set; }
        public virtual DbSet<TAB_ANIMATEUR> TAB_ANIMATEUR { get; set; }
        public virtual DbSet<TAB_ART> TAB_ART { get; set; }
        public virtual DbSet<TAB_ART_OLD> TAB_ART_OLD { get; set; }
        public virtual DbSet<TAB_CADRE> TAB_CADRE { get; set; }
        public virtual DbSet<TAB_CADRE_OLD> TAB_CADRE_OLD { get; set; }
        public virtual DbSet<TAB_CATEG> TAB_CATEG { get; set; }
        public virtual DbSet<TAB_CATEG_OLD> TAB_CATEG_OLD { get; set; }
        public virtual DbSet<TAB_CHAMPS> TAB_CHAMPS { get; set; }
        public virtual DbSet<TAB_CONSID_VAR> TAB_CONSID_VAR { get; set; }
        public virtual DbSet<TAB_CONSIDERANTS> TAB_CONSIDERANTS { get; set; }
        public virtual DbSet<TAB_CORPS> TAB_CORPS { get; set; }
        public virtual DbSet<TAB_CORPS_OLD> TAB_CORPS_OLD { get; set; }
        public virtual DbSet<TAB_DECORATIONS> TAB_DECORATIONS { get; set; }
        public virtual DbSet<TAB_DEP> TAB_DEP { get; set; }
        public virtual DbSet<TAB_E_E_I_D> TAB_E_E_I_D { get; set; }
        public virtual DbSet<TAB_E_E_I_D_OLD> TAB_E_E_I_D_OLD { get; set; }
        public virtual DbSet<TAB_ECHELLE> TAB_ECHELLE { get; set; }
        public virtual DbSet<TAB_ECHELON> TAB_ECHELON { get; set; }
        public virtual DbSet<TAB_EMPLOI> TAB_EMPLOI { get; set; }
        public virtual DbSet<TAB_ENREG_COMMUNICATION> TAB_ENREG_COMMUNICATION { get; set; }
        public virtual DbSet<TAB_ENTSIGL> TAB_ENTSIGL { get; set; }
        public virtual DbSet<TAB_ERREUR_PB> TAB_ERREUR_PB { get; set; }
        public virtual DbSet<TAB_EXERCICE> TAB_EXERCICE { get; set; }
        public virtual DbSet<TAB_FCT> TAB_FCT { get; set; }
        public virtual DbSet<TAB_G_E_I_D> TAB_G_E_I_D { get; set; }
        public virtual DbSet<TAB_G_E_I_D_OLD> TAB_G_E_I_D_OLD { get; set; }
        public virtual DbSet<TAB_GARDE> TAB_GARDE { get; set; }
        public virtual DbSet<TAB_GRADE> TAB_GRADE { get; set; }
        public virtual DbSet<TAB_GRADE_OLD> TAB_GRADE_OLD { get; set; }
        public virtual DbSet<TAB_GRSIGL> TAB_GRSIGL { get; set; }
        public virtual DbSet<TAB_IMP_BUD> TAB_IMP_BUD { get; set; }
        public virtual DbSet<TAB_LANGUE> TAB_LANGUE { get; set; }
        public virtual DbSet<TAB_LIEN_PAR> TAB_LIEN_PAR { get; set; }
        public virtual DbSet<TAB_LOC> TAB_LOC { get; set; }
        public virtual DbSet<TAB_MESSAGE> TAB_MESSAGE { get; set; }
        public virtual DbSet<TAB_MOD_AV_GRADE> TAB_MOD_AV_GRADE { get; set; }
        public virtual DbSet<TAB_MOUVEMENT_DS> TAB_MOUVEMENT_DS { get; set; }
        public virtual DbSet<TAB_NAT> TAB_NAT { get; set; }
        public virtual DbSet<TAB_NIVEAU_INST> TAB_NIVEAU_INST { get; set; }
        public virtual DbSet<TAB_ORG_DET> TAB_ORG_DET { get; set; }
        public virtual DbSet<TAB_ORGANISATEUR> TAB_ORGANISATEUR { get; set; }
        public virtual DbSet<TAB_PARAMETRES> TAB_PARAMETRES { get; set; }
        public virtual DbSet<TAB_PAYS> TAB_PAYS { get; set; }
        public virtual DbSet<TAB_PB_DS> TAB_PB_DS { get; set; }
        public virtual DbSet<TAB_POS> TAB_POS { get; set; }
        public virtual DbSet<TAB_PRF_CONJ> TAB_PRF_CONJ { get; set; }
        public virtual DbSet<TAB_PROFILES> TAB_PROFILES { get; set; }
        public virtual DbSet<TAB_REP_SAL> TAB_REP_SAL { get; set; }
        public virtual DbSet<TAB_ROLES> TAB_ROLES { get; set; }
        public virtual DbSet<TAB_S_ORD> TAB_S_ORD { get; set; }
        public virtual DbSet<TAB_S_ST> TAB_S_ST { get; set; }
        public virtual DbSet<TAB_SAF> TAB_SAF { get; set; }
        public virtual DbSet<TAB_SAF_FTF> TAB_SAF_FTF { get; set; }
        public virtual DbSet<TAB_SAF_FTF_OLD> TAB_SAF_FTF_OLD { get; set; }
        public virtual DbSet<TAB_SAF_LOC> TAB_SAF_LOC { get; set; }
        public virtual DbSet<TAB_SAF_OLD> TAB_SAF_OLD { get; set; }
        public virtual DbSet<TAB_SEQUENCE_SIGL> TAB_SEQUENCE_SIGL { get; set; }
        public virtual DbSet<TAB_SEQUENCES> TAB_SEQUENCES { get; set; }
        public virtual DbSet<TAB_SIT_CONJ> TAB_SIT_CONJ { get; set; }
        public virtual DbSet<TAB_SIT_F_AG> TAB_SIT_F_AG { get; set; }
        public virtual DbSet<TAB_SIT_PAC> TAB_SIT_PAC { get; set; }
        public virtual DbSet<TAB_SPECIALITE> TAB_SPECIALITE { get; set; }
        public virtual DbSet<TAB_SPECIALITE_FORMATION> TAB_SPECIALITE_FORMATION { get; set; }
        public virtual DbSet<TAB_SRV> TAB_SRV { get; set; }
        public virtual DbSet<TAB_SRV_VAL> TAB_SRV_VAL { get; set; }
        public virtual DbSet<TAB_SS_TYP_MVT> TAB_SS_TYP_MVT { get; set; }
        public virtual DbSet<TAB_TACHES> TAB_TACHES { get; set; }
        public virtual DbSet<TAB_TYP_MENTION> TAB_TYP_MENTION { get; set; }
        public virtual DbSet<TAB_TYP_MVT> TAB_TYP_MVT { get; set; }
        public virtual DbSet<TAB_TYP_MVT_LC> TAB_TYP_MVT_LC { get; set; }
        public virtual DbSet<TAB_TYPE_FORMATION> TAB_TYPE_FORMATION { get; set; }
        public virtual DbSet<TAB_TYPES_CONGES> TAB_TYPES_CONGES { get; set; }
        public virtual DbSet<TABLES_DS_MVT> TABLES_DS_MVT { get; set; }
        public virtual DbSet<TACHE_AGENT> TACHE_AGENT { get; set; }
        public virtual DbSet<TEMP_AGENT_PB> TEMP_AGENT_PB { get; set; }
        public virtual DbSet<TEMP_PB> TEMP_PB { get; set; }
        public virtual DbSet<TEMP_SYS> TEMP_SYS { get; set; }
        public virtual DbSet<TEST_VIS_CHAMPS_FORM> TEST_VIS_CHAMPS_FORM { get; set; }
        public virtual DbSet<TITRE> TITREs { get; set; }
        public virtual DbSet<TRANCHES_REFERENCES> TRANCHES_REFERENCES { get; set; }
        public virtual DbSet<TRANCHES_REFERENCES_> TRANCHES_REFERENCES_ { get; set; }
        public virtual DbSet<TT> TTs { get; set; }
        public virtual DbSet<UTILISATEUR> UTILISATEURS { get; set; }
        public virtual DbSet<VIS_CHAMPS_FORM> VIS_CHAMPS_FORM { get; set; }
    }
}
