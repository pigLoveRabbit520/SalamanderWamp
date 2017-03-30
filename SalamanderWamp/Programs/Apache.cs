using SalamanderWamp.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace SalamanderWamp.Programs
{
    class Apache : WampProgram
    {
        private readonly ServiceController apacheController = new ServiceController();
        public const string ServiceName = "apache-salamander";

        public Apache()
        {
            apacheController.MachineName = Environment.MachineName;
            apacheController.ServiceName = ServiceName;
        }

        public void RemoveService()
        {
            StartProcess("cmd.exe", stopArgs, true);
        }


        public void InstallService()
        {
            StartProcess(exeName, startArgs, true);
        }

        public bool ServiceExists()
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (var service in services)
            {
                if (service.ServiceName == ServiceName)
                    return true;
            }
            return false;
        }

        public override void Start()
        {
            try
            {
                if (IsRunning())
                    return;
            }
            catch (Exception ex)
            {
                Log.wnmp_log_notice("You need to be the administrator to Start Mysql Service", progLogSection);
            }
            try
            {
                apacheController.Start();
                apacheController.WaitForStatus(ServiceControllerStatus.Running);
                Log.wnmp_log_notice("Started " + progName, progLogSection);
            }
            catch (Exception ex)
            {
                Log.wnmp_log_error("Start(): " + ex.Message, progLogSection);
            }
        }

        public override void Stop()
        {
            if (isStopped())
            {
                return;
            }
            try
            {
                apacheController.Stop();
                apacheController.WaitForStatus(ServiceControllerStatus.Stopped);
                Log.wnmp_log_notice("Stopped " + progName, progLogSection);
            }
            catch (Exception ex)
            {
                Log.wnmp_log_notice("Stop(): " + ex.Message, progLogSection);
            }
        }


        /// <summary>
        /// 通过ServiceController判断服务是否在运行
        /// </summary>
        /// <returns></returns>
        public override bool IsRunning()
        {
            try
            {
                return apacheController.Status == ServiceControllerStatus.Running;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 通过ServiceController判断服务是否停止
        /// </summary>
        /// <returns></returns>
        private bool isStopped()
        {
            return apacheController.Status == ServiceControllerStatus.Stopped;
        }


    }

}
