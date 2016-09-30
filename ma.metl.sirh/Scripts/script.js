/**
* Fonctions Javascript spécifique à l'application
*/

$(function() {
    $( "#datepicker,.datepicker,.datepicker1" ).datepicker(
		{ 	changeMonth: true,
			changeYear: true}
	);
	$.datepicker.regional[ "fr" ];
	$( "#dateFrom,.dateFrom" ).datepicker({
      changeMonth: true,
	  changeYear: true,
      numberOfMonths: 1,
      onClose: function( selectedDate ) {
        $( "#dateTo,.dateTo" ).datepicker( "option", "minDate", selectedDate );
      }
    });
	$( "#dateTo,.dateTo" ).datepicker({
	  changeMonth: true,
	  changeYear: true,
	  numberOfMonths: 1,
	  onClose: function( selectedDate ) {
		$( "#dateFrom,.dateFrom" ).datepicker( "option", "maxDate", selectedDate );
	  }
	});
	
	$(".dateMonthYear,#dateMonthYear").datepicker({
		dateFormat: 'mm/yy',
		changeMonth: true,
		changeYear: true,
		showButtonPanel: true,
		onClose: function (dateText, inst) {
			var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
			var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
			$(this).datepicker('setDate', new Date(year, month, 1));
		},
		beforeShow: function (input, inst) {
			if ((datestr = $(this).val()).length > 0) {
				actDate = datestr.split('/');
				year = actDate[1];
				month = actDate[0] - 1;
				$(this).datepicker('option', 'defaultDate', new Date(year, month));
				$(this).datepicker('setDate', new Date(year, month));
			}
		}
	});
	$(".dateMonthYear,#dateMonthYear").focus(function () {
		$(".ui-datepicker-calendar").hide();
		$("#ui-datepicker-div").position({
			my: "center top",
			at: "center bottom",
			of: $(this)
		});
	});
	
	/* Nouvelle demande ou réclamation */
	$("#wrapperDelegation").hide(50);

	/*if $("select[name='typeDemande']").val()==1) {
		$("#wrapperDelegation").show(60);		
	}
	else {
		$("#wrapperDelegation").hide(50);		
	}
*/
	$("select[name='typeDemande']").change(function () {
	    if ($(this).val() == 1) {
	        
		    $("#wrapperDelegation").show(60);
		   
		    $("#nouvelleDemande").load('/GestionReclamations/NouvelleDeclaration');
		}
	  else if ($(this).val() == 2) {
	       
			$("#wrapperDelegation").hide(50);
			$("#nouvelleDemande").load('/GestionReclamations/NouvelleReclamation');
	    }
	    else {
	        $("#wrapperDelegation").hide(50);
	        $("#nouvelleDemande").load('/GestionReclamations/NouvelleCession');
	    }
	});
	
	typePersonneProcuration ();
	
  });
 /* Personne Physiqu ou Morale  et procuration */ 
 function typePersonneProcuration ()
 {
     $("#personnePhysique,#personneMorale,#procuration,#personneMoraleAcheteur, #personnePhysiqueAcheteur").hide();
	
	/*if ($("input[name='typePersonne']").val()=="Physique") {
		$("#personnePhysique").show(60);
		$("#personneMorale").hide(50);
	}
	else if ($("input[name='typePersonne']").val()=="Morale") {
		$("#personneMorale").show(60);
		$("#personnePhysique").hide(50);
	}
	*/
	if ($("input[name='procuration']").val()=="Oui") {
		$("#procuration").show(60);
	}
	else if ($("input[name='procuration']").val()=="Non") {
		$("#procuration").hide(50);
	}
	
	
	$("input[name='typePersonne']").change(function(){		
		if ($(this).val()=="Physique") {
			$("#personnePhysique").show(60);
			$("#personneMorale").hide(50);
		}
		else if ($(this).val()=="Morale") {
			$("#personneMorale").show(60);
			$("#personnePhysique").hide(50);
		}
	});
	$("input[name='procuration']").change(function(){
		if ($(this).val()=="Oui") {
			$("#procuration").show(60);
		}
		else if ($(this).val()=="Non") {
			$("#procuration").hide(50);
		}
	});

	$("input[name='typeAcheteur']").change(function () {
	    if ($(this).val() == "Physique") {
	        $("#personnePhysiqueAcheteur").show(60);
	        $("#personneMoraleAcheteur").hide(50);
	    }
	    else if ($(this).val() == "Morale") {
	        $("#personneMoraleAcheteur").show(60);
	        $("#personnePhysiqueAcheteur").hide(50);
	    }
	});
 } 
/* Lancement de la popup */
function popup(id)
{
	$('#'+id).modal('show');
}
/* Lancement de la popup lightbox */
$(document).delegate('*[data-toggle="lightbox"]', 'click', function(event) {
    event.preventDefault();
    $(this).ekkoLightbox({loadingMessage:"Chargement ..."});
}); 

/* Affichage info propriétaire */
function showInfoProprio (){
	$("#infoProprio").removeClass('hide');	
	$("#infoProprio").addClass('show');	
}
/* Affichage photo */
function showPhoto (){
	$("#photo").removeClass('hide');	
	$("#photo").addClass('show');	
}
