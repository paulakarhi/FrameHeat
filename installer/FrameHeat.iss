; Installer for FrameHeat. Build it with scripts\build-installer.ps1.
;
; The language chosen on the first wizard page is written to the registry, and the program
; reads it at start. PawnIO, the driver that makes CPU and memory temperatures readable, is
; installed silently when it is missing.

#ifndef AppVersion
  #define AppVersion "0.2.0"
#endif
#define AppName "FrameHeat"
#define AppExe "FrameHeat.exe"

[Setup]
AppId={{9880A873-752E-42BD-8EBA-A7D279410059}
AppName={#AppName}
AppVersion={#AppVersion}
VersionInfoVersion={#AppVersion}
DefaultDirName={autopf}\{#AppName}
DisableProgramGroupPage=yes
UninstallDisplayIcon={app}\{#AppExe}
SetupIconFile=..\src\FrameHeat\Assets\app.ico
OutputDir=..\dist-installer
OutputBaseFilename={#AppName}-Setup-{#AppVersion}
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
; The driver and the HKLM language value need administrator rights. A per-user install
; (/CURRENTUSER) is only offered on the command line, for scripted installs.
PrivilegesRequired=admin
PrivilegesRequiredOverridesAllowed=commandline
CloseApplications=yes
RestartApplications=no

[Languages]
Name: "en"; MessagesFile: "compiler:Default.isl"
Name: "fi"; MessagesFile: "compiler:Languages\Finnish.isl"
Name: "sv"; MessagesFile: "compiler:Languages\Swedish.isl"
Name: "de"; MessagesFile: "compiler:Languages\German.isl"
Name: "fr"; MessagesFile: "compiler:Languages\French.isl"
Name: "es"; MessagesFile: "compiler:Languages\Spanish.isl"
Name: "pt"; MessagesFile: "compiler:Languages\BrazilianPortuguese.isl"
Name: "zh"; MessagesFile: "compiler:Default.isl,Languages\ChineseSimplified.isl"
Name: "ja"; MessagesFile: "compiler:Languages\Japanese.isl"
Name: "ko"; MessagesFile: "compiler:Languages\Korean.isl"

[CustomMessages]
en.InstallingDriver=Installing the PawnIO driver (needed for CPU and memory temperatures)...
fi.InstallingDriver=Asennetaan PawnIO-ajuria (tarvitaan suorittimen ja muistin lämpötiloihin)...
sv.InstallingDriver=Installerar PawnIO-drivrutinen (behövs för processor- och minnestemperaturer)...
de.InstallingDriver=PawnIO-Treiber wird installiert (erforderlich für CPU- und Speichertemperaturen)...
fr.InstallingDriver=Installation du pilote PawnIO (nécessaire pour les températures du processeur et de la mémoire)...
es.InstallingDriver=Instalando el controlador PawnIO (necesario para las temperaturas del procesador y la memoria)...
pt.InstallingDriver=Instalando o driver PawnIO (necessário para as temperaturas do processador e da memória)...
zh.InstallingDriver=正在安装 PawnIO 驱动程序（读取处理器和内存温度所需）...
ja.InstallingDriver=PawnIO ドライバーをインストールしています（CPU とメモリの温度の取得に必要）...
ko.InstallingDriver=PawnIO 드라이버를 설치하는 중입니다(CPU 및 메모리 온도 확인에 필요)...

en.DriverFailed=The PawnIO driver could not be installed. FrameHeat will still run, but CPU, disk and memory temperatures will not be shown. You can install the driver later from https://pawnio.eu/.
fi.DriverFailed=PawnIO-ajurin asennus epäonnistui. FrameHeat toimii silti, mutta suorittimen, levyjen ja muistin lämpötiloja ei näytetä. Voit asentaa ajurin myöhemmin osoitteesta https://pawnio.eu/.
sv.DriverFailed=PawnIO-drivrutinen kunde inte installeras. FrameHeat fungerar ändå, men temperaturer för processor, diskar och minne visas inte. Du kan installera drivrutinen senare från https://pawnio.eu/.
de.DriverFailed=Der PawnIO-Treiber konnte nicht installiert werden. FrameHeat läuft trotzdem, zeigt aber keine Temperaturen von CPU, Datenträgern und Speicher an. Sie können den Treiber später von https://pawnio.eu/ installieren.
fr.DriverFailed=Le pilote PawnIO n'a pas pu être installé. FrameHeat fonctionnera quand même, mais les températures du processeur, des disques et de la mémoire ne seront pas affichées. Vous pourrez installer le pilote plus tard depuis https://pawnio.eu/.
es.DriverFailed=No se pudo instalar el controlador PawnIO. FrameHeat funcionará igualmente, pero no se mostrarán las temperaturas del procesador, los discos ni la memoria. Puede instalar el controlador más tarde desde https://pawnio.eu/.
pt.DriverFailed=Não foi possível instalar o driver PawnIO. O FrameHeat continuará funcionando, mas as temperaturas do processador, dos discos e da memória não serão exibidas. Você pode instalar o driver mais tarde em https://pawnio.eu/.
zh.DriverFailed=无法安装 PawnIO 驱动程序。FrameHeat 仍可运行，但不会显示处理器、磁盘和内存的温度。您可以稍后从 https://pawnio.eu/ 安装该驱动程序。
ja.DriverFailed=PawnIO ドライバーをインストールできませんでした。FrameHeat は動作しますが、CPU・ディスク・メモリの温度は表示されません。ドライバーは後から https://pawnio.eu/ でインストールできます。
ko.DriverFailed=PawnIO 드라이버를 설치하지 못했습니다. FrameHeat는 실행되지만 CPU, 디스크, 메모리 온도는 표시되지 않습니다. 나중에 https://pawnio.eu/ 에서 드라이버를 설치할 수 있습니다.

[Files]
Source: "..\dist\{#AppExe}"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\LICENSE"; DestDir: "{app}"; DestName: "LICENSE.txt"; Flags: ignoreversion
Source: "..\THIRD-PARTY-NOTICES.md"; DestDir: "{app}"; DestName: "THIRD-PARTY-NOTICES.txt"; Flags: ignoreversion
Source: "..\licenses\*"; DestDir: "{app}\licenses"; Flags: ignoreversion
Source: "redist\PawnIO_setup.exe"; Flags: dontcopy

[Icons]
Name: "{autoprograms}\{#AppName}"; Filename: "{app}\{#AppExe}"

[Registry]
; HKA is HKLM for an administrator install and HKCU for a per-user one; the program reads both.
Root: HKA; Subkey: "Software\{#AppName}"; ValueType: string; ValueName: "Language"; ValueData: "{language}"; Flags: uninsdeletekey

[Run]
Filename: "{app}\{#AppExe}"; Description: "{cm:LaunchProgram,{#AppName}}"; Flags: nowait postinstall skipifsilent runascurrentuser

[UninstallRun]
Filename: "{sys}\taskkill.exe"; Parameters: "/IM {#AppExe} /F"; Flags: runhidden; RunOnceId: "StopApp"
Filename: "{sys}\schtasks.exe"; Parameters: "/Delete /TN ""{#AppName}"" /F"; Flags: runhidden; RunOnceId: "RemoveAutostart"

[Code]
const
  RebootRequired = 3010;

var
  RestartNeeded: Boolean;

function PawnIoMissing: Boolean;
begin
  Result := not RegKeyExists(HKLM, 'SYSTEM\CurrentControlSet\Services\PawnIO');
end;

procedure InstallDriver;
var
  ResultCode: Integer;
  Started: Boolean;
begin
  WizardForm.StatusLabel.Caption := CustomMessage('InstallingDriver');
  WizardForm.FilenameLabel.Caption := '';

  ExtractTemporaryFile('PawnIO_setup.exe');
  Started := Exec(ExpandConstant('{tmp}\PawnIO_setup.exe'), '-install -silent', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);

  if Started and (ResultCode = RebootRequired) then
    RestartNeeded := True
  else if (not Started) or (ResultCode <> 0) then
    SuppressibleMsgBox(CustomMessage('DriverFailed'), mbError, MB_OK, IDOK);
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  if (CurStep = ssPostInstall) and IsAdminInstallMode and PawnIoMissing then
    InstallDriver;
end;

function NeedRestart: Boolean;
begin
  Result := RestartNeeded;
end;
