using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;

namespace MyTunes
{
	public static class SongLoader
	{
		const string Filename = "songs.json";

		public static async Task<IEnumerable<Song>> Load()
		{
			using (var reader = new StreamReader(await OpenData()))
			{
				return JsonConvert.DeserializeObject<List<Song>>(await reader.ReadToEndAsync());
			}
		}

		private async static Task<Stream> OpenData()
		{
		#if __IOS__

			return await Task.FromResult(File.OpenRead(Filename));

		#elif __ANDROID__

			return await Task.FromResult(Android.App.Application.Context.Assets.Open(Filename));

		#elif WINDOWS_PHONE_APP

			var sf = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(Filename);
			return await sf.OpenStreamForReadAsync();

		#elif WINDOWS_UWP

			var sf = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(Filename);
			return await sf.OpenStreamForReadAsync();

		#endif
		}
	}
}

