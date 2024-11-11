helm install oathkeeper\
    -f ./config/oathkeeper/config.yaml \
    --set-file 'oathkeeper.accessRules=./config/oathkeeper/rules.json' \
    ory/oathkeeper