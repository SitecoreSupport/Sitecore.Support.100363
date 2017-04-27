using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.Save;

namespace Sitecore.Support.Pipelines.Save
{
  public class DecodeDoubleQuoteInLink
  {
    public void Process(SaveArgs args)
    {
      if (Context.Site != null && Context.PageMode.IsExperienceEditor && Client.ContentDatabase != null)
      {
        Assert.ArgumentNotNull(args, "args");
        Assert.IsNotNull(args.Items, "args.Items");

        SaveArgs.SaveItem[] items = args.Items;

        foreach (var item in items)
        {
          Item item2 = Client.ContentDatabase.Items[item.ID, item.Language, item.Version];

          if (null != item2)
          {
            foreach (var field in item.Fields)
            {
              Field field2 = item2.Fields[field.ID];
              if (((field2 != null) && !field2.IsBlobField))
              {
                string str = field.Value;
                if (!string.IsNullOrEmpty(str))
                {
                  field.Value = str.Replace("-,sc_dq,-", "&quot;");
                }
              }
            }
          }
        }
      }
    }
  }
}
