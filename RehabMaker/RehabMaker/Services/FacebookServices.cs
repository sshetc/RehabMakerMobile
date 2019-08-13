using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RehabMaker.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RehabMaker.Services
{
    public class FacebookServices
    {

        public async Task<FacebookProfile> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl =
                "https://graph.facebook.com/v3.2/me/?fields=name,picture,locale,link,cover,age_range,devices,first_name,last_name,gender,is_verified" +
                "&access_token="
                + accessToken;

            var httpClient = new HttpClient();



            var userJson = await httpClient.GetStringAsync(requestUrl);

            var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);

            return facebookProfile;
        }
    }
}
