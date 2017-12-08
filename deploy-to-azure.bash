#!/bin/bash -ex

# This script utilizes a less secure way of triggering deployment to Azure Web Apps.
# - It calls the Kudu service and provides it with the Git account credendials so that
# - Kudu can pull source files from the specified repository. This operation is 
#   synchronous (blocks until either the 'fetch' deployment completes successfully or errors out).
# NOTE: This approach is not recommended for production deployments as it is not very secure.
#       It is only provided here as an example of the different deployment options available.
#
# These variables should be defined in the Builds environment variables.
# Retrieve them from an Azure Web App's Publishing Profile (.publishsettings).
# - FTP_PASSWORD
# - SITE_NAME (e.g.: webappname)
#
# In addition, the following environment variable for the Bitbucket account are needed:
# - BITBUCKET_USERNAME
# - BITBUCKET_PASSWORD (check the 'Secured' flag)
# - REPOSITORY_NAME (e.g. my-code-repo)

# Trigger the Kudu service of the Azure Web App:
# NOTE: If the FTP_PASSWORD and BITBUCKET_PASSWORD environment variables were added with the "Secured" checkbox checked,
#       then the passwords will not be shown in cleartext in the build logs, and will instead be replaced
#       with the "$FTP_PASSWORD" and "BITBUCKET_PASSWORD" tokens. Another cool feature of Bitbucket Pipelines.

# Trigger a manual (fetch) Kudu deployemnt:
curl -X POST "https://\$$SITE_NAME:$FTP_PASSWORD@$SITE_NAME.scm.azurewebsites.net/deploy" \
  --header "Content-Type: application/json" \
  --header "Accept: application/json" \
  --header "X-SITE-DEPLOYMENT-ID: $SITE_NAME" \
  --header "Transfer-encoding: chunked" \
  --data "{\"format\":\"basic\", \"url\":\"https://$BITBUCKET_USERNAME:$BITBUCKET_PASSWORD@bitbucket.org/$BITBUCKET_USERNAME/$REPOSITORY_NAME.git\"}"

echo Finished uploading files to site $SITE_NAME.
