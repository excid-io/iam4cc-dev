# **Kubernetes & Helm Scripts**

This folder contains scripts for automating the setup, deployment, and cleanup of the Helm Charts related to the IAM4CC project. 
---

## **1. Start Minikube**

### Script: `start_minikube.sh`

This script sets up and starts a Minikube Kubernetes cluster. It also enables necessary Minikube features like **Ingress** for routing traffic.

**Usage:**
```bash
./start_minikube.sh
```

---

## **2. Install ORY Hydra**

### Script: `install_hydra_helm.sh`

This script deploys the ORY Hydra service using Helm. Hydra provides OAuth 2.0 and OpenID Connect support for authentication.

**Usage:**
```bash
./install_hydra_helm.sh
```

---

## **3. Install ORY Oathkeeper**

### Script: `install_oathkeeper_helm.sh`

This script deploys ORY Oathkeeper with the necessary configuration for the IAM4CC project, which you can find in the `config` folder.

**Usage:**
```bash
./install_oathkeeper_helm.sh
```

---

## **4. Install OpenFGA**

### Script: `install_openfga_helm.sh`

This script deploys OpenFGA.

**Usage:**
```bash
./install_openfga_helm.sh
```

---

## **5. Deploy the Policy Decision Point (PDP)**

### Script: `deploy_pdp.sh`

This script builds and deploys the custom Helm Chart for the Policy Decision Point component of the implementation. Note that it switches to minikube's docker daemon to build the PDP docker image. Ypu can use `eval $(minikube docker-env -u)` to reset your Docker environment back to your default Docker daemon.

**Usage:**
```bash
./deploy_pdp.sh
```

---

## **6. Deploy the Registry**

### Script: `deploy_registry.sh`

This script builds and deploys the custom Helm Chart for the Registry component of the implementation. Note that it switches to minikube's docker daemon to build the PDP docker image. You can use `eval $(minikube docker-env -u)` to reset your Docker environment back to your default Docker daemon.

**Usage:**
```bash
./deploy_registry.sh
```

---

## **7. Clean Up Minikube and Helm**

### Script: `cleanup_minikube_helm.sh`

This script stops using Minikube's Docker daemon, uninstalls all Helm releases, and ensures no residual resources remain. Note that to avoid uninstalling releases unrelated to the IAM4CC project, you may need to uninstall them manually if you installed them manually with different release names.

**Usage:**
```bash
./cleanup_minikube_helm.sh
```

---

## **8. Stop Minikube**

### Script: `stop_minikube.sh`

This script stops and deletes the Minikube Kubernetes cluster. Note that the helm releases are not deleted in this way, and if you use `start_minikube.sh` again, you do not need to do the installations again.

**Usage:**
```bash
./stop_minikube.sh
```

--- 

## Troubleshooting

### Port-Related Issues

Sometimes, ports required by the implementation may already be in use or not properly released, causing errors like "connection refused" or port forwarding failures. 

### 1. Verify ports in use

**Usage:**
```bash
sudo lsof -i -P -n | grep LISTEN
```

Look for the ports related to the implementation (6000, 6002, 6003, 6004, 6005).

### 2. Release ports

**Usage:**
```bash
sudo lsof -i :6004 -t | xargs -r kill -9
```

### 3. Stop All Kubernetes Port-Forwarding

If multiple port-forwarding sessions are active, they may conflict. Terminate all kubectl port-forward processes:

**Usage:**
```bash
pkill -f "kubectl port-forward"
```

Note that this will affect all kubectl port-forward processes in your system.
