using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Indanet.neXus.Debugging;
using System.ServiceModel.Web;

namespace wcfRestLib
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true,
    InstanceContextMode = InstanceContextMode.Single,
    ConcurrencyMode = ConcurrencyMode.Single)]
    [ServiceContract]
    public class VideoProviderRestApi
    {
        WardTrace TRACE;
        VideoProvider m_Provider;
        public VideoProviderRestApi(VideoProvider provider, Uri baseUri, WardTrace parentTrace)
        {
            TRACE = new WardTrace("VideoProviderRestApi: "+baseUri.ToString(), parentTrace, null);

            m_Provider = provider;

            TRACE.Info("VideoProviderRestApi created!");
        }

        [WebInvoke(Method = "POST", UriTemplate = "/CompilationEvents")]
        [OperationContract]
        public void PostEvent(ProviderEvent newEvent)
        {
            try
            {
                TRACE.Function("PostEvent(): ENTER. newEvent[{0}]", newEvent);

                //TODO: AG: Send it to real Storage

                TRACE.Info("PostEvent(): SUCCEEDED. newEvent[{0}]", newEvent);
            }
            catch (Exception ex)
            {
                TRACE.Error("PostEvent(): FAILED. newEvent[{0}]\n{1}", newEvent, ex);
                //TODO: AG: What to do here? 
                //throw;
            }
            finally
            {
                TRACE.Function("PostEvent(): LEAVE. newEvent[{0}]", newEvent);
            }
        }

        [WebGet(UriTemplate = "/Provider" ,ResponseFormat=WebMessageFormat.Json)]
        public VideoProviderState GetProviderState()
        {
            VideoProviderState state = new VideoProviderState();
            state.OperationMode = VideoProviderOperationMode.Recording;
            state.OperationState = VideoProviderOperationState.Recording;

            for (int i = 0; i < 10; i++)
                AddCameraToProviderState(state, "cam" + i);

            TRACE.Info("GetProviderState called!");

            return state;
        }

        private static void AddCameraToProviderState(VideoProviderState state, string camName)
        {
            VideoProviderCameraState cam = new VideoProviderCameraState();
            cam.CameraRecordingMode = CameraRecordingMode.Recording;
            cam.CameraOperationState = CameraOperationState.Recording;
            cam.CameraSystemName = camName;
            state.CameraStates.Add( cam );
        }
    }
}
