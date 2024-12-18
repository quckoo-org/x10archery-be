{{- define "default.name" -}}
{{- default .Chart.Name .Values.nameOverride | trunc 63 | trimSuffix "-" }}
{{- end }}

{{- define "default.fullname" -}}
{{- if .Values.fullnameOverride -}}
{{- .Values.fullnameOverride | trunc 63 | trimSuffix "-" -}}
{{- else -}}
{{- $name := default .Chart.Name .Values.nameOverride -}}
{{- if contains $name .Release.Name -}}
{{- .Release.Name | trunc 63 | trimSuffix "-" -}}
{{- else -}}
{{- printf "%s-%s" .Release.Name $name | trunc 63 | trimSuffix "-" -}}
{{- end -}}
{{- end -}}
{{- end -}}

{{- define "default.chart" -}}
{{- printf "%s-%s" .Chart.Name .Chart.Version | replace "+" "_" | trunc 63 | trimSuffix "-" }}
{{- end }}

{{- define "default.labels" -}}
helm.sh/chart: {{ include "default.chart" . }}
{{ include "default.selectorLabels" . }}
{{- if .Chart.AppVersion }}
app.kubernetes.io/version: {{ .Chart.AppVersion | quote }}
{{- end }}
app.kubernetes.io/managed-by: {{ .Release.Service }}
{{- end }}

{{- define "default.selectorLabels" -}}
app.kubernetes.io/name: {{ include "default.name" . }}
app.kubernetes.io/instance: {{ .Release.Name }}
{{- end }}

{{- define "pod.labels" -}}
{{- if .Values.labels.pod }}
{{- toYaml .Values.labels.pod }}
{{- end }}
{{- end }}

{{- define "deploy.labels" -}}
{{- if .Values.labels.deploy }}
{{- toYaml .Values.labels.deploy }}
{{- end }}
{{- end }}

{{- define "httpproxy.labels" -}}
{{- if .Values.labels.httpproxy }}
{{- toYaml .Values.labels.httpproxy }}
{{- end }}
{{- end }}