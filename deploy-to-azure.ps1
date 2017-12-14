
$uri = "https://`$$Env:SITE_NAME:$Env:FTP_PASSWORD@$Env:SITE_NAME.scm.azurewebsites.net/deploy?scmType=GitHub"
$headers = @{ }
$headers.Add("Content-Type", "application/json")
$headers.Add("Accept", "application/json")
$headers.Add("X-SITE-DEPLOYMENT-ID", $Env:SITE_NAME)
$headers.Add("Transfer-encoding", "chunked")
$body = @{ }
$body.Add("format", "basic")
$body.Add("url", "https://$Env:GITHUB_USERNAME:$Env:GITHUB_PASSWORD@GITHUB.org/$Env:GITHUB_USERNAME/$Env:REPOSITORY_NAME.git")

Invoke-WebRequest -Uri $uri -Method "POST" -Headers $headers -Body $body