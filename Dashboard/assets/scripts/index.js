async function initializeIndex(){
  await validateUser();
  await checkRoleClaim();
}

async function checkRoleClaim() {
  var jwtToken = localStorage.getItem('jwtToken');
  
  if (jwtToken) {
    var tokenParts = jwtToken.split('.');
    var base64Url = tokenParts[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var decodedToken = JSON.parse(atob(base64));
    
    var role = decodedToken.Role;
    if (role === 'Admin' || role === 'Viewer') {
      await startSignalR();
      healthCheck();
    } 
    else{
      $('#index-error-message').text('You dont have enough permissions. Contact an administrator.')
    } 
  } 
}
  