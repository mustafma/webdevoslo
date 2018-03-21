#set your kubectl context to docker for desktop (local k8s)
kubectl config get-contexts #to see what you have
kubectl config use-context docker-for-desktop #to set to the correct one if you have a bunch of clusters you manage..

#to get a dashboard
kubectl apply -f https://raw.githubusercontent.com/kubernetes/dashboard/master/src/deploy/alternative/kubernetes-dashboard.yaml

#run the dashboard
kubectl proxy

#url for the dashboard
http://127.0.0.1:8001/api/v1/proxy/namespaces/kube-system/services/kubernetes-dashboard/#!/overview?namespace=default

#make sure iis isnt running locally otherwise nothing will work!!

#only nasty is the image pull policy....always doesnt work as it tries to pull from dockerhub....
#so you need to remove the images locally to ensure a new one really gets pulled