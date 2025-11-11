<#!
.SYNOPSIS
  날짜별 콘솔 로그 파일에 메시지를 기록합니다.

.DESCRIPTION
  저장소 루트의 `logs/console-YYYY-MM-DD.log`에 라인 단위로 메시지를 추가합니다.

.USAGE
  "출력 내용" | .specify\scripts\powershell\write-log.ps1
  .specify\scripts\powershell\write-log.ps1 -Message "출력 내용"
  some-command | .specify\scripts\powershell\write-log.ps1
#>

[CmdletBinding()]
param(
  [Parameter(ValueFromPipeline=$true, ValueFromRemainingArguments=$true)]
  [string[]]$Message
)

begin {
  $dateStr = Get-Date -Format 'yyyy-MM-dd'
  $repoRoot = Resolve-Path (Join-Path (Join-Path (Join-Path $PSScriptRoot '..') '..') '..')
  $logsDir = Join-Path $repoRoot 'logs'
  if (-not (Test-Path $logsDir)) { New-Item -ItemType Directory -Path $logsDir | Out-Null }
  $logPath = Join-Path $logsDir ("console-{0}.log" -f $dateStr)
}

process {
  if ($null -ne $Message) {
    foreach ($line in $Message) {
      if ($null -ne $line -and $line -ne '') {
        $timestamp = (Get-Date).ToString('yyyy-MM-dd HH:mm:ss')
        Add-Content -Path $logPath -Value ("[{0}] {1}" -f $timestamp, $line)
      }
    }
  }
}

end {
  # 완료 표시를 위해 경로 한 줄 출력
  Write-Output $logPath
}
