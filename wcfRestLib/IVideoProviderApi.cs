using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

using Indanet.neXus.Wcf;

namespace wcfRestLib
{
    /// <summary>
    /// Interface for 3rd party applications with reduced dependencies
    /// </summary>
    [ServiceContract]
    public interface IVideoProviderApiServer : IKeepAlive
    {
        #region Modes / States

        [OperationContract]
        bool SetOperationMode(VideoProviderOperationMode operationMode);
        [OperationContract]
        VideoProviderOperationMode GetOperationMode();
        [OperationContract]
        VideoProviderOperationState GetOperationState();

        [OperationContract]
        bool SetCameraRecordingMode(string cameraSystemName, CameraRecordingMode recordingMode);
        [OperationContract]
        CameraRecordingMode GetCameraRecordingMode(string cameraSystemName);
        [OperationContract]
        CameraOperationState GetCameraOperationState(string cameraSystemName);
        [OperationContract]
        VideoProviderCameraState GetCameraState(string cameraSystemName);
        [OperationContract]
        bool SetAllCameraRecordingModes(CameraRecordingMode recordingMode);
        [OperationContract]
        List<VideoProviderCameraState> GetAllCameraStates();

        [OperationContract]
        VideoProviderState GetVideoProviderState();

        [OperationContract]
        List<VideoProviderReceiverPerfInfo> GetAllCameraReceiver1PerfInfos();
        [OperationContract]
        List<VideoProviderReceiverPerfInfo> GetAllCameraReceiver2PerfInfos();

        #endregion

        #region Locks

        [OperationContract]
        Guid LockLoopRecordings(DateTime lockStartTimestampUtc, DateTime lockEndTimestampUtc);
        [OperationContract]
        void UnlockLoopRecordings(Guid lockId);
        [OperationContract]
        void RemoveLoopRecordingLocks(DateTime lockStartTimestampUtc, DateTime lockEndTimestampUtc);

        #endregion

        #region Events

        [OperationContract]
        bool RecordEvent(DateTime startTimestampUtc, DateTime endTimestampUtc, string eventSource, string eventType, string eventDescription);
        [OperationContract]
        bool RecordCameraEvent(string cameraSystemName, DateTime startTimestampUtc, DateTime endTimestampUtc, string eventSource, string eventType, string eventDescription);

        #endregion

        #region Camera Time

        [OperationContract]
        bool SetCameraTimeUtc(string cameraSystemName, DateTime timeUtc);
        [OperationContract]
        bool SetAllCameraTimesUtc(DateTime timeUtc);
        [OperationContract]
        bool SetCameraTimeLocal(string cameraSystemName, DateTime timeLocal);
        [OperationContract]
        bool SetAllCameraTimesLocal(DateTime timeLocal);

        #endregion

        #region Camera Overlay

        [OperationContract]
        bool SetCameraOverlayText(string cameraSystemName, string text);
        [OperationContract]
        bool SetCameraOverlayTextValue(string cameraSystemName, string key, string value);
        [OperationContract]
        bool SetAllCameraOverlayTexts(string text);
        [OperationContract]
        bool SetAllCameraOverlayTextValues(string key, string value);

        #endregion

        #region Camera Control

        [OperationContract]
        bool ConfigureCamera(string cameraSystemName);
        [OperationContract]
        bool ConfigureAllCameras();
        [OperationContract]
        bool RestartCamera(string cameraSystemName);
        [OperationContract]
        bool RestartAllCameras();

        #endregion
    }

    [Serializable]
    [DataContract]
    public enum VideoProviderOperationMode
    {
        [EnumMember]
        Unknown = 0,

        [EnumMember]
        Offline = 1,
        [EnumMember]
        Stopped = 2,
        [EnumMember]
        CleanupOnly = 3,
        [EnumMember]
        Recording = 4
    }

    [Serializable]
    [DataContract]
    public enum VideoProviderOperationState
    {
        [EnumMember]
        Unknown = 0,

        [EnumMember]
        Offline = 1,
        [EnumMember]
        Stopped = 2,
        [EnumMember]
        CleanupOnly = 3,
        [EnumMember]
        Recording = 4,
        [EnumMember]
        MediumFailure = 5
    }

    [Serializable]
    [DataContract]
    public enum CameraRecordingMode
    {
        [EnumMember]
        Unknown = 0,

        [EnumMember]
        Stopped = 1,
        [EnumMember]
        Recording = 2
    }

    [Serializable]
    [DataContract]
    public enum CameraOperationState
    {
        [EnumMember]
        Unknown = 0,

        [EnumMember]
        Offline = 2,
        [EnumMember]
        Online = 3,
        [EnumMember]
        Recording = 4,
        [EnumMember]
        MediumFailure = 5
    }

    [Serializable]
    [DataContract]
    public enum CameraAlertType
    {
        [EnumMember]
        Unknown = 0,

        [EnumMember]
        Tampering = 1,

        [EnumMember]
        MotionDetection = 2
    }

    [Serializable]
    [DataContract]
    public class VideoProviderCameraState
    {
        [DataMember]
        public string CameraSystemName;
        [DataMember]
        public CameraRecordingMode CameraRecordingMode;
        [DataMember]
        public CameraOperationState CameraOperationState;

        public VideoProviderCameraState()
        {
            CameraSystemName = "";
            CameraRecordingMode = CameraRecordingMode.Unknown;
            CameraOperationState = CameraOperationState.Unknown;
        }

        #region ToString

        public override string ToString()
        {
            return string.Format("Camera[{0}] RecordingMode[{1}] State[{2}]",
                CameraSystemName, CameraRecordingMode, CameraOperationState);
        }

        #endregion
    }

    [Serializable]
    [DataContract]
    public class VideoProviderState
    {
        [DataMember]
        public VideoProviderOperationMode OperationMode;
        [DataMember]
        public VideoProviderOperationState OperationState;
        [DataMember]
        public List<VideoProviderCameraState> CameraStates;

        public VideoProviderState()
        {
            OperationMode = VideoProviderOperationMode.Unknown;
            OperationState = VideoProviderOperationState.Unknown;
            CameraStates = new List<VideoProviderCameraState>();
        }

        #region ToString

        public override string ToString()
        {
            StringBuilder buf = new StringBuilder(1024);

            buf.AppendFormat("\nOperationMode[{0}]", OperationMode);
            buf.AppendFormat("\nOperationState[{0}]", OperationState);

            foreach (VideoProviderCameraState cameraState in CameraStates)
            {
                buf.AppendFormat("\n  {0}", cameraState);
            }

            return buf.ToString();
        }

        #endregion
    }

    [Serializable]
    [DataContract]
    public class VideoProviderReceiverPerfInfo
    {
        [DataMember]
        public string CameraSystemName;
        [DataMember]
        public string PerfInfoInstanceName;

        [DataMember]
        public float FramesReceived;
        [DataMember]
        public float FramesReceivedPerSec;
        [DataMember]
        public float FrameDataReceived;
        [DataMember]
        public float FrameDataReceivedPerSec;

        [DataMember]
        public float VideoFramesReceived;
        [DataMember]
        public float VideoFramesReceivedPerSec;
        [DataMember]
        public float VideoFrameDataReceived;
        [DataMember]
        public float VideoFrameDataReceivedPerSec;

        [DataMember]
        public float UnknownFramesReceived;
        [DataMember]
        public float UnknownFramesReceivedPerSec;
        [DataMember]
        public float UnknownFrameDataReceived;
        [DataMember]
        public float UnknownFrameDataReceivedPerSec;

        [DataMember]
        public float SysHeadersReceived;
        [DataMember]
        public float SysHeadersReceivedPerSec;

        [DataMember]
        public float IFramesReceived;
        [DataMember]
        public float IFramesReceivedPerSec;

        [DataMember]
        public float BPFramesReceived;
        [DataMember]
        public float BPFramesReceivedPerSec;

        [DataMember]
        public float CodecType;

        [DataMember]
        public float IsOnline;
    }
}

