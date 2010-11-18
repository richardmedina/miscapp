function clickButton(e, buttonid)
{

      var evt = e ? e : window.event;

      var bt = document.getElementById(buttonid);

      if (bt){

          if (evt.keyCode == 13){

                bt.click();

                return false;

          }

      }

}

function focusControl (e, buttonid)
{
	var evt = e ? e : window.event;

	var bt = document.getElementById(buttonid);

	if (bt) {
		if (evt.keyCode == 13){
            bt.focus();
            return false;
		}
      }
}

function popup (location, width, height)
{
	window.open (location, 'info', 'height=' + height + ',width=' + width + ',menubar=NO,location=no,resizable= yes,scrollbars=yes,status=yes')
}
