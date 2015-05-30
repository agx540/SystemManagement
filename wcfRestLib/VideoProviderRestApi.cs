using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace wcfRestLib
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true,
    InstanceContextMode = InstanceContextMode.Single,
    ConcurrencyMode = ConcurrencyMode.Single)]
    [ServiceContract]
    public class VideoProviderRestApi
    {
        int m_CamCount = 1;
        VideoProvider m_Provider;
        public VideoProviderRestApi(VideoProvider provider, Uri baseUri, int camCount)
        {
            m_Provider = provider;
            m_CamCount = camCount;

            Console.WriteLine(string.Format("VideoProviderRestApi created! CamCount: {0}", m_CamCount));
        }

        [WebInvoke(Method = "POST", UriTemplate = "/CompilationEvents")]
        [OperationContract]
        public void PostEvent(ProviderEvent newEvent)
        {
            try
            {
                Console.WriteLine(string.Format("PostEvent(): ENTER. newEvent[{0}]", newEvent));

                //TODO: AG: Send it to real Storage

                Console.WriteLine(string.Format("PostEvent(): SUCCEEDED. newEvent[{0}]", newEvent));
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("PostEvent(): FAILED. newEvent[{0}]\n{1}", newEvent, ex));
                //TODO: AG: What to do here? 
                //throw;
            }
            finally
            {
                Console.WriteLine(string.Format("PostEvent(): LEAVE. newEvent[{0}]", newEvent));
            }
        }

        [WebGet(UriTemplate = "/Status" ,ResponseFormat=WebMessageFormat.Json)]
        public VideoProviderState GetProviderState()
        {
            VideoProviderState state = new VideoProviderState();
            state.OperationMode = VideoProviderOperationMode.Recording;
            state.OperationState = VideoProviderOperationState.Recording;

            for (int i = 0; i < m_CamCount; i++)
                AddCameraToProviderState(state, "cam" + i);

            Console.WriteLine(string.Format("GetProviderState called!"));

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
