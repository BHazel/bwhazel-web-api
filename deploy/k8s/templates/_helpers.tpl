{{/*
Expand the name of the chart.
*/}}
{{- define "bwhazel-api.name" -}}
{{- default .Chart.Name .Values.nameOverride | trunc 63 | trimSuffix "-" }}
{{- end }}

{{/*
Create a default fully qualified app name.
The name is truncated at 63 characters because some Kubernetes name fields
are limited to this by the DNS naming spec.  If release name contains chart
name it will be used as the full name.
*/}}
{{- define "bwhazel-api.fullname" -}}
{{- if .Values.fullnameOverride }}
{{- .Values.fullnameOverride | trunc 63 | trimSuffix "-" }}
{{- else }}
{{- $name := default .Chart.Name .Values.nameOverride }}
{{- if contains $name .Release.Name }}
{{- .Release.Name | trunc 63 | trimSuffix "-" }}
{{- else }}
{{- printf "%s-%s" .Release.Name $name | trunc 63 | trimSuffix "-" }}
{{- end }}
{{- end }}
{{- end }}

{{/*
Create chart name and version as used by the chart label.
*/}}
{{- define "bwhazel-api.chart" -}}
{{- printf "%s-%s" .Chart.Name .Chart.Version | replace "+" "_" | trunc 63 | trimSuffix "-" }}
{{- end }}

{{/*
Common labels.
*/}}
{{- define "bwhazel-api.labels" -}}
helm.sh/chart: {{ include "bwhazel-api.chart" . }}
{{ include "bwhazel-api.selectorLabels" . }}
{{- if .Chart.AppVersion }}
app.kubernetes.io/version: {{ .Chart.AppVersion | quote }}
{{- end }}
app.kubernetes.io/managed-by: {{ .Release.Service }}
{{- end }}

{{/*
Selector labels.
*/}}
{{- define "bwhazel-api.selectorLabels" -}}
app.kubernetes.io/name: {{ include "bwhazel-api.name" . }}
app.kubernetes.io/instance: {{ .Release.Name }}
{{- end }}