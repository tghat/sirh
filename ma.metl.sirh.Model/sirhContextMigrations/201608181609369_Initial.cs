namespace ma.metl.sirh.Model.sirhContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AutorisationProfil",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Autorisation_Id = c.Long(nullable: false),
                        Profil_Id = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Autorisation", t => t.Autorisation_Id, cascadeDelete: true)
                .ForeignKey("dbo.Profil", t => t.Profil_Id, cascadeDelete: true)
                .Index(t => t.Autorisation_Id)
                .Index(t => t.Profil_Id);
            
            CreateTable(
                "dbo.Autorisation",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                        Module_Id = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Module", t => t.Module_Id, cascadeDelete: true)
                .Index(t => t.Module_Id);
            
            CreateTable(
                "dbo.Module",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Profil",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Libelle = c.String(),
                        Role = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Candidat",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NumDoti = c.String(),
                        Nom = c.String(),
                        Prenom = c.String(),
                        NomAR = c.String(),
                        PrenomAR = c.String(),
                        Email = c.String(),
                        TelPersonnel = c.String(),
                        Sexe = c.String(),
                        CIN = c.String(),
                        DateNaissance = c.DateTime(),
                        DateRecrutement = c.DateTime(),
                        DateEffet = c.DateTime(),
                        DirectionId = c.Long(nullable: false),
                        GradeId = c.Long(nullable: false),
                        LocaliteId = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Direction", t => t.DirectionId, cascadeDelete: true)
                .ForeignKey("dbo.Grade", t => t.GradeId, cascadeDelete: true)
                .ForeignKey("dbo.Localite", t => t.LocaliteId, cascadeDelete: true)
                .Index(t => t.DirectionId)
                .Index(t => t.GradeId)
                .Index(t => t.LocaliteId);
            
            CreateTable(
                "dbo.DetailAvancement",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Annee = c.String(),
                        DateAncienGrade = c.DateTime(nullable: false),
                        DateEff = c.DateTime(nullable: false),
                        Note = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Statut = c.String(),
                        DecisionCap = c.String(),
                        motifDecision = c.String(),
                        AncienneteGrade = c.DateTime(nullable: false),
                        AncienneteEchelon = c.DateTime(nullable: false),
                        CandidatId = c.Long(nullable: false),
                        FluxId = c.Long(nullable: false),
                        GradeIdAncien = c.Long(),
                        GradeIdNouveau = c.Long(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grade", t => t.GradeIdAncien)
                .ForeignKey("dbo.Candidat", t => t.CandidatId, cascadeDelete: true)
                .ForeignKey("dbo.Flux", t => t.FluxId, cascadeDelete: true)
                .ForeignKey("dbo.Grade", t => t.GradeIdNouveau)
                .Index(t => t.CandidatId)
                .Index(t => t.FluxId)
                .Index(t => t.GradeIdAncien)
                .Index(t => t.GradeIdNouveau);
            
            CreateTable(
                "dbo.Grade",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        DescriptionAM = c.String(),
                        DescriptionAF = c.String(),
                        CodeCateg = c.String(),
                        CodeCadre = c.String(),
                        CodeCorps = c.String(),
                        CodeGrade = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Flux",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TypeFlux = c.String(),
                        Source = c.String(),
                        nbrTotalLigne = c.Int(nullable: false),
                        nbrLigneRejete = c.Int(nullable: false),
                        flux = c.String(),
                        name = c.String(),
                        dateIntegration = c.DateTime(nullable: false),
                        anneeReception = c.String(),
                        Etat = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LigneRejetee",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        detail = c.String(),
                        motifRejet = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                        Flux_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Flux", t => t.Flux_Id)
                .Index(t => t.Flux_Id);
            
            CreateTable(
                "dbo.Candidature",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Etat = c.String(),
                        Formulaire = c.String(),
                        Annee = c.String(),
                        CentreId = c.Long(),
                        CandidatId = c.Long(nullable: false),
                        GradeIdNouveau = c.Long(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Candidat", t => t.CandidatId, cascadeDelete: true)
                .ForeignKey("dbo.CentreExamen", t => t.CentreId)
                .ForeignKey("dbo.Grade", t => t.GradeIdNouveau)
                .Index(t => t.CentreId)
                .Index(t => t.CandidatId)
                .Index(t => t.GradeIdNouveau);
            
            CreateTable(
                "dbo.CentreExamen",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        name = c.String(),
                        adresse = c.String(),
                        LocaliteId = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Localite", t => t.LocaliteId, cascadeDelete: true)
                .Index(t => t.LocaliteId);
            
            CreateTable(
                "dbo.Localite",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Intitule = c.String(),
                        IntituleA = c.String(),
                        IdOrd = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Direction",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                        DescriptionA = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Historique",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Action = c.String(),
                        Message = c.String(),
                        DateAction = c.DateTime(nullable: false),
                        NomComplet = c.String(),
                        UserId = c.Long(),
                        DetailId = c.Long(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                        Candidat_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DetailAvancement", t => t.DetailId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Candidat", t => t.Candidat_Id)
                .Index(t => t.UserId)
                .Index(t => t.DetailId)
                .Index(t => t.Candidat_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Prenom = c.String(),
                        Email = c.String(),
                        ProfilId = c.Long(nullable: false),
                        DirectionId = c.Long(nullable: false),
                        ServiceId = c.Long(nullable: false),
                        Identifiant = c.String(),
                        Statut = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        ConfirmationPassword = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Direction", t => t.DirectionId, cascadeDelete: true)
                .ForeignKey("dbo.Profil", t => t.ProfilId, cascadeDelete: true)
                .ForeignKey("dbo.Service", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ProfilId)
                .Index(t => t.DirectionId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Service",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sanction",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        DateEffet = c.DateTime(),
                        Reference = c.Int(),
                        AnneeActe = c.String(),
                        CandidatId = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Candidat", t => t.CandidatId, cascadeDelete: true)
                .Index(t => t.CandidatId);
            
            CreateTable(
                "dbo.Commission",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Titre = c.String(),
                        Annee = c.String(),
                        EcritOrOral = c.String(),
                        GradeId = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grade", t => t.GradeId, cascadeDelete: true)
                .Index(t => t.GradeId);
            
            CreateTable(
                "dbo.CommissionMembre",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Commission_Id = c.Long(nullable: false),
                        MembreCap_Id = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Commission", t => t.Commission_Id, cascadeDelete: true)
                .ForeignKey("dbo.MembreCommission", t => t.MembreCap_Id, cascadeDelete: true)
                .Index(t => t.Commission_Id)
                .Index(t => t.MembreCap_Id);
            
            CreateTable(
                "dbo.MembreCommission",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NDoti = c.String(),
                        nom = c.String(),
                        nomA = c.String(),
                        prenom = c.String(),
                        prenomA = c.String(),
                        DirectionId = c.Long(nullable: false),
                        GradeId = c.Long(),
                        LocaliteId = c.Long(nullable: false),
                        Email = c.String(),
                        Tel = c.String(),
                        Adresse = c.String(),
                        Interim = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Direction", t => t.DirectionId, cascadeDelete: true)
                .ForeignKey("dbo.Grade", t => t.GradeId)
                .ForeignKey("dbo.Localite", t => t.LocaliteId, cascadeDelete: true)
                .Index(t => t.DirectionId)
                .Index(t => t.GradeId)
                .Index(t => t.LocaliteId);
            
            CreateTable(
                "dbo.Convocation",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Objet = c.String(),
                        Message = c.String(),
                        ConvocationFile = c.String(),
                        MembreCap_Id = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MembreCommission", t => t.MembreCap_Id, cascadeDelete: true)
                .Index(t => t.MembreCap_Id);
            
            CreateTable(
                "dbo.Examen",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Intitule = c.String(),
                        GradeId = c.Long(nullable: false),
                        DirectionId = c.Long(nullable: false),
                        NoteEliminatoire = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MoyennePassage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Annee = c.String(),
                        Datelimite = c.DateTime(),
                        DateExamen = c.DateTime(),
                        nbrCandidatEligibleAnnee = c.Int(nullable: false),
                        nbrCandidatEligibleDateExam = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Direction", t => t.DirectionId, cascadeDelete: true)
                .ForeignKey("dbo.Grade", t => t.GradeId, cascadeDelete: true)
                .Index(t => t.GradeId)
                .Index(t => t.DirectionId);
            
            CreateTable(
                "dbo.MatiereExamen",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Examen_Id = c.Long(nullable: false),
                        Matiere_Id = c.Long(nullable: false),
                        Coefficient = c.Int(nullable: false),
                        DateMatiere = c.DateTime(),
                        DureeH = c.String(),
                        DureeM = c.String(),
                        HeureDebutH = c.String(),
                        HeureDebutM = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Examen", t => t.Examen_Id, cascadeDelete: true)
                .ForeignKey("dbo.Matiere", t => t.Matiere_Id, cascadeDelete: true)
                .Index(t => t.Examen_Id)
                .Index(t => t.Matiere_Id);
            
            CreateTable(
                "dbo.Matiere",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Intitule = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExamenCentreExamen",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Examen_Id = c.Long(nullable: false),
                        CentreExamen_Id = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CentreExamen", t => t.CentreExamen_Id, cascadeDelete: true)
                .ForeignKey("dbo.Examen", t => t.Examen_Id, cascadeDelete: true)
                .Index(t => t.Examen_Id)
                .Index(t => t.CentreExamen_Id);
            
            CreateTable(
                "dbo.Notation",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NoteEcrite = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NotePresConnaissanceG = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NoteConnaissMin = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NoteConnaissSp = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NoteOrale = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NoteGlobale = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                        DetailAvancement_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DetailAvancement", t => t.DetailAvancement_Id)
                .Index(t => t.DetailAvancement_Id);
            
            CreateTable(
                "dbo.ParametrageClassement",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Critere = c.String(),
                        Valeur = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Annee = c.String(),
                        TypeFlux = c.String(),
                        GradeId = c.Long(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grade", t => t.GradeId)
                .Index(t => t.GradeId);
            
            CreateTable(
                "dbo.ParametrageQuota",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        GradeIdAcces = c.Long(),
                        GradeIdOccupe = c.Long(),
                        Anciennete = c.String(),
                        Annee = c.String(),
                        Quota = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NbrPoste = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TypeFlux = c.String(),
                        Statut = c.String(),
                        Commentaire = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grade", t => t.GradeIdAcces)
                .ForeignKey("dbo.Grade", t => t.GradeIdOccupe)
                .Index(t => t.GradeIdAcces)
                .Index(t => t.GradeIdOccupe);
            
            CreateTable(
                "dbo.Parametre",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                        TypeParametre = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Publication",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TypePublication = c.String(),
                        Objet = c.String(),
                        DateDebutPub = c.DateTime(nullable: false),
                        DateFinPub = c.DateTime(nullable: false),
                        PieceJointePub = c.Binary(),
                        Statut = c.Int(nullable: false),
                        ContentType = c.String(),
                        FileName = c.String(),
                        Type = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reunion",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OrdreJour = c.String(),
                        OrdreJourAr = c.String(),
                        Observation = c.String(),
                        Decisions = c.String(),
                        DateReunion = c.DateTime(nullable: false),
                        Lieu = c.String(),
                        Pv = c.String(),
                        DureeString = c.String(),
                        Duree = c.Int(nullable: false),
                        CommissionId = c.Long(nullable: false),
                        StatusENUM = c.String(),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Commission", t => t.CommissionId, cascadeDelete: true)
                .Index(t => t.CommissionId);
            
            CreateTable(
                "dbo.Specialite",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Intitule = c.String(),
                        GradeId = c.Long(),
                        ExamenId = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Examen", t => t.ExamenId, cascadeDelete: true)
                .ForeignKey("dbo.Grade", t => t.GradeId)
                .Index(t => t.GradeId)
                .Index(t => t.ExamenId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Specialite", "GradeId", "dbo.Grade");
            DropForeignKey("dbo.Specialite", "ExamenId", "dbo.Examen");
            DropForeignKey("dbo.Reunion", "CommissionId", "dbo.Commission");
            DropForeignKey("dbo.ParametrageQuota", "GradeIdOccupe", "dbo.Grade");
            DropForeignKey("dbo.ParametrageQuota", "GradeIdAcces", "dbo.Grade");
            DropForeignKey("dbo.ParametrageClassement", "GradeId", "dbo.Grade");
            DropForeignKey("dbo.Notation", "DetailAvancement_Id", "dbo.DetailAvancement");
            DropForeignKey("dbo.ExamenCentreExamen", "Examen_Id", "dbo.Examen");
            DropForeignKey("dbo.ExamenCentreExamen", "CentreExamen_Id", "dbo.CentreExamen");
            DropForeignKey("dbo.MatiereExamen", "Matiere_Id", "dbo.Matiere");
            DropForeignKey("dbo.MatiereExamen", "Examen_Id", "dbo.Examen");
            DropForeignKey("dbo.Examen", "GradeId", "dbo.Grade");
            DropForeignKey("dbo.Examen", "DirectionId", "dbo.Direction");
            DropForeignKey("dbo.Convocation", "MembreCap_Id", "dbo.MembreCommission");
            DropForeignKey("dbo.CommissionMembre", "MembreCap_Id", "dbo.MembreCommission");
            DropForeignKey("dbo.MembreCommission", "LocaliteId", "dbo.Localite");
            DropForeignKey("dbo.MembreCommission", "GradeId", "dbo.Grade");
            DropForeignKey("dbo.MembreCommission", "DirectionId", "dbo.Direction");
            DropForeignKey("dbo.CommissionMembre", "Commission_Id", "dbo.Commission");
            DropForeignKey("dbo.Commission", "GradeId", "dbo.Grade");
            DropForeignKey("dbo.Sanction", "CandidatId", "dbo.Candidat");
            DropForeignKey("dbo.Candidat", "LocaliteId", "dbo.Localite");
            DropForeignKey("dbo.Historique", "Candidat_Id", "dbo.Candidat");
            DropForeignKey("dbo.Historique", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "ServiceId", "dbo.Service");
            DropForeignKey("dbo.Users", "ProfilId", "dbo.Profil");
            DropForeignKey("dbo.Users", "DirectionId", "dbo.Direction");
            DropForeignKey("dbo.Historique", "DetailId", "dbo.DetailAvancement");
            DropForeignKey("dbo.Candidat", "GradeId", "dbo.Grade");
            DropForeignKey("dbo.Candidat", "DirectionId", "dbo.Direction");
            DropForeignKey("dbo.Candidature", "GradeIdNouveau", "dbo.Grade");
            DropForeignKey("dbo.Candidature", "CentreId", "dbo.CentreExamen");
            DropForeignKey("dbo.CentreExamen", "LocaliteId", "dbo.Localite");
            DropForeignKey("dbo.Candidature", "CandidatId", "dbo.Candidat");
            DropForeignKey("dbo.DetailAvancement", "GradeIdNouveau", "dbo.Grade");
            DropForeignKey("dbo.DetailAvancement", "FluxId", "dbo.Flux");
            DropForeignKey("dbo.LigneRejetee", "Flux_Id", "dbo.Flux");
            DropForeignKey("dbo.DetailAvancement", "CandidatId", "dbo.Candidat");
            DropForeignKey("dbo.DetailAvancement", "GradeIdAncien", "dbo.Grade");
            DropForeignKey("dbo.AutorisationProfil", "Profil_Id", "dbo.Profil");
            DropForeignKey("dbo.AutorisationProfil", "Autorisation_Id", "dbo.Autorisation");
            DropForeignKey("dbo.Autorisation", "Module_Id", "dbo.Module");
            DropIndex("dbo.Specialite", new[] { "ExamenId" });
            DropIndex("dbo.Specialite", new[] { "GradeId" });
            DropIndex("dbo.Reunion", new[] { "CommissionId" });
            DropIndex("dbo.ParametrageQuota", new[] { "GradeIdOccupe" });
            DropIndex("dbo.ParametrageQuota", new[] { "GradeIdAcces" });
            DropIndex("dbo.ParametrageClassement", new[] { "GradeId" });
            DropIndex("dbo.Notation", new[] { "DetailAvancement_Id" });
            DropIndex("dbo.ExamenCentreExamen", new[] { "CentreExamen_Id" });
            DropIndex("dbo.ExamenCentreExamen", new[] { "Examen_Id" });
            DropIndex("dbo.MatiereExamen", new[] { "Matiere_Id" });
            DropIndex("dbo.MatiereExamen", new[] { "Examen_Id" });
            DropIndex("dbo.Examen", new[] { "DirectionId" });
            DropIndex("dbo.Examen", new[] { "GradeId" });
            DropIndex("dbo.Convocation", new[] { "MembreCap_Id" });
            DropIndex("dbo.MembreCommission", new[] { "LocaliteId" });
            DropIndex("dbo.MembreCommission", new[] { "GradeId" });
            DropIndex("dbo.MembreCommission", new[] { "DirectionId" });
            DropIndex("dbo.CommissionMembre", new[] { "MembreCap_Id" });
            DropIndex("dbo.CommissionMembre", new[] { "Commission_Id" });
            DropIndex("dbo.Commission", new[] { "GradeId" });
            DropIndex("dbo.Sanction", new[] { "CandidatId" });
            DropIndex("dbo.Users", new[] { "ServiceId" });
            DropIndex("dbo.Users", new[] { "DirectionId" });
            DropIndex("dbo.Users", new[] { "ProfilId" });
            DropIndex("dbo.Historique", new[] { "Candidat_Id" });
            DropIndex("dbo.Historique", new[] { "DetailId" });
            DropIndex("dbo.Historique", new[] { "UserId" });
            DropIndex("dbo.CentreExamen", new[] { "LocaliteId" });
            DropIndex("dbo.Candidature", new[] { "GradeIdNouveau" });
            DropIndex("dbo.Candidature", new[] { "CandidatId" });
            DropIndex("dbo.Candidature", new[] { "CentreId" });
            DropIndex("dbo.LigneRejetee", new[] { "Flux_Id" });
            DropIndex("dbo.DetailAvancement", new[] { "GradeIdNouveau" });
            DropIndex("dbo.DetailAvancement", new[] { "GradeIdAncien" });
            DropIndex("dbo.DetailAvancement", new[] { "FluxId" });
            DropIndex("dbo.DetailAvancement", new[] { "CandidatId" });
            DropIndex("dbo.Candidat", new[] { "LocaliteId" });
            DropIndex("dbo.Candidat", new[] { "GradeId" });
            DropIndex("dbo.Candidat", new[] { "DirectionId" });
            DropIndex("dbo.Autorisation", new[] { "Module_Id" });
            DropIndex("dbo.AutorisationProfil", new[] { "Profil_Id" });
            DropIndex("dbo.AutorisationProfil", new[] { "Autorisation_Id" });
            DropTable("dbo.Specialite");
            DropTable("dbo.Reunion");
            DropTable("dbo.Publication");
            DropTable("dbo.Parametre");
            DropTable("dbo.ParametrageQuota");
            DropTable("dbo.ParametrageClassement");
            DropTable("dbo.Notation");
            DropTable("dbo.ExamenCentreExamen");
            DropTable("dbo.Matiere");
            DropTable("dbo.MatiereExamen");
            DropTable("dbo.Examen");
            DropTable("dbo.Convocation");
            DropTable("dbo.MembreCommission");
            DropTable("dbo.CommissionMembre");
            DropTable("dbo.Commission");
            DropTable("dbo.Sanction");
            DropTable("dbo.Service");
            DropTable("dbo.Users");
            DropTable("dbo.Historique");
            DropTable("dbo.Direction");
            DropTable("dbo.Localite");
            DropTable("dbo.CentreExamen");
            DropTable("dbo.Candidature");
            DropTable("dbo.LigneRejetee");
            DropTable("dbo.Flux");
            DropTable("dbo.Grade");
            DropTable("dbo.DetailAvancement");
            DropTable("dbo.Candidat");
            DropTable("dbo.Profil");
            DropTable("dbo.Module");
            DropTable("dbo.Autorisation");
            DropTable("dbo.AutorisationProfil");
        }
    }
}
