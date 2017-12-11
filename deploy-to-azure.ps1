
$uri = "https://\$$env.SITE_NAME:$env.FTP_PASSWORD@$env.SITE_NAME.scm.azurewebsites.net/deploy?scmType=GitHub"
$headers = @{ }
$headers.Add("Content-Type", "application/json")
$headers.Add("Accept", "application/json")
$headers.Add("X-SITE-DEPLOYMENT-ID", $env.SITE_NAME)
$headers.Add("Transfer-encoding", "chunked")
$body = @{ }
$body.Add("format", "basic")
$body.Add("url", "https://$env.GITHUB_USERNAME:$env.GITHUB_PASSWORD@GITHUB.org/$env.GITHUB_USERNAME/$env.REPOSITORY_NAME.git")

Invoke-WebRequest -Uri $uri -Method "POST" -Headers $headers -Body $body