using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Data.OData;
using System.IO;
using System.Xml.Linq;

namespace OData4Soda.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class SodaToODataConverterTests
    {
        #region raw JSON data
        private const string JsonText = @"{
  ""meta"" : {
    ""view"" : {
      ""id"" : ""z8bn-74gv"",
      ""name"" : ""Police Stations"",
      ""attribution"" : ""Chicago Police Department"",
      ""attributionLink"" : ""http://www.chicagopolice.org/"",
      ""averageRating"" : 0,
      ""category"" : ""Public Safety"",
      ""createdAt"" : 1293052023,
      ""description"" : ""Chicago Police district station locations and contact information."",
      ""downloadCount"" : 309,
      ""numberOfComments"" : 0,
      ""oid"" : 306934,
      ""publicationAppendEnabled"" : false,
      ""publicationDate"" : 1302998589,
      ""publicationGroup"" : 220749,
      ""publicationStage"" : ""published"",
      ""rowClass"" : """",
      ""rowsUpdatedAt"" : 1313894852,
      ""rowsUpdatedBy"" : ""scy9-9wg4"",
      ""signed"" : false,
      ""tableId"" : 220749,
      ""totalTimesRated"" : 0,
      ""viewCount"" : 18598,
      ""viewLastModified"" : 1314569087,
      ""viewType"" : ""tabular"",
      ""columns"" : [ {
        ""id"" : -1,
        ""name"" : ""sid"",
        ""dataTypeName"" : ""meta_data"",
        ""fieldName"" : ""sid"",
        ""position"" : 0,
        ""renderTypeName"" : ""meta_data"",
        ""format"" : {
        }
      }, {
        ""id"" : -1,
        ""name"" : ""id"",
        ""dataTypeName"" : ""meta_data"",
        ""fieldName"" : ""id"",
        ""position"" : 0,
        ""renderTypeName"" : ""meta_data"",
        ""format"" : {
        }
      }, {
        ""id"" : -1,
        ""name"" : ""position"",
        ""dataTypeName"" : ""meta_data"",
        ""fieldName"" : ""position"",
        ""position"" : 0,
        ""renderTypeName"" : ""meta_data"",
        ""format"" : {
        }
      }, {
        ""id"" : -1,
        ""name"" : ""created_at"",
        ""dataTypeName"" : ""meta_data"",
        ""fieldName"" : ""created_at"",
        ""position"" : 0,
        ""renderTypeName"" : ""meta_data"",
        ""format"" : {
        }
      }, {
        ""id"" : -1,
        ""name"" : ""created_meta"",
        ""dataTypeName"" : ""meta_data"",
        ""fieldName"" : ""created_meta"",
        ""position"" : 0,
        ""renderTypeName"" : ""meta_data"",
        ""format"" : {
        }
      }, {
        ""id"" : -1,
        ""name"" : ""updated_at"",
        ""dataTypeName"" : ""meta_data"",
        ""fieldName"" : ""updated_at"",
        ""position"" : 0,
        ""renderTypeName"" : ""meta_data"",
        ""format"" : {
        }
      }, {
        ""id"" : -1,
        ""name"" : ""updated_meta"",
        ""dataTypeName"" : ""meta_data"",
        ""fieldName"" : ""updated_meta"",
        ""position"" : 0,
        ""renderTypeName"" : ""meta_data"",
        ""format"" : {
        }
      }, {
        ""id"" : -1,
        ""name"" : ""meta"",
        ""dataTypeName"" : ""meta_data"",
        ""fieldName"" : ""meta"",
        ""position"" : 0,
        ""renderTypeName"" : ""meta_data"",
        ""format"" : {
        }
      }, {
        ""id"" : 2616478,
        ""name"" : ""DISTRICT"",
        ""dataTypeName"" : ""text"",
        ""fieldName"" : ""district"",
        ""position"" : 1,
        ""renderTypeName"" : ""text"",
        ""tableColumnId"" : 1428202,
        ""width"" : 50,
        ""cachedContents"" : {
          ""non_null"" : 26,
          ""smallest"" : ""1"",
          ""null"" : 0,
          ""largest"" : ""Headquarters"",
          ""top"" : [ {
            ""count"" : 1,
            ""item"" : ""3""
          }, {
            ""count"" : 1,
            ""item"" : ""14""
          }, {
            ""count"" : 1,
            ""item"" : ""17""
          }, {
            ""count"" : 1,
            ""item"" : ""6""
          }, {
            ""count"" : 1,
            ""item"" : ""1""
          }, {
            ""count"" : 1,
            ""item"" : ""5""
          }, {
            ""count"" : 1,
            ""item"" : ""16""
          }, {
            ""count"" : 1,
            ""item"" : ""24""
          }, {
            ""count"" : 1,
            ""item"" : ""Headquarters""
          }, {
            ""count"" : 1,
            ""item"" : ""11""
          }, {
            ""count"" : 1,
            ""item"" : ""15""
          }, {
            ""count"" : 1,
            ""item"" : ""4""
          }, {
            ""count"" : 1,
            ""item"" : ""23""
          }, {
            ""count"" : 1,
            ""item"" : ""12""
          }, {
            ""count"" : 1,
            ""item"" : ""7""
          }, {
            ""count"" : 1,
            ""item"" : ""18""
          }, {
            ""count"" : 1,
            ""item"" : ""2""
          }, {
            ""count"" : 1,
            ""item"" : ""20""
          }, {
            ""count"" : 1,
            ""item"" : ""25""
          }, {
            ""count"" : 1,
            ""item"" : ""21""
          } ]
        },
        ""format"" : {
          ""align"" : ""right""
        }
      }, {
        ""id"" : 2609802,
        ""name"" : ""ADDRESS"",
        ""dataTypeName"" : ""text"",
        ""fieldName"" : ""address"",
        ""position"" : 2,
        ""renderTypeName"" : ""text"",
        ""tableColumnId"" : 1424829,
        ""width"" : 140,
        ""cachedContents"" : {
          ""non_null"" : 26,
          ""smallest"" : ""100 S Racine Ave"",
          ""null"" : 0,
          ""largest"" : ""937 N Wood St"",
          ""top"" : [ {
            ""count"" : 1,
            ""item"" : ""5701 W Madison St""
          }, {
            ""count"" : 1,
            ""item"" : ""6464 N Clark St""
          }, {
            ""count"" : 1,
            ""item"" : ""3151 W Harrison St""
          }, {
            ""count"" : 1,
            ""item"" : ""1160 N Larrabee St""
          }, {
            ""count"" : 1,
            ""item"" : ""1438 W 63rd St""
          }, {
            ""count"" : 1,
            ""item"" : ""5555 W Grand Ave""
          }, {
            ""count"" : 1,
            ""item"" : ""5101 S Wentworth Ave""
          }, {
            ""count"" : 1,
            ""item"" : ""3420 W 63rd St""
          }, {
            ""count"" : 1,
            ""item"" : ""100 S Racine Ave""
          }, {
            ""count"" : 1,
            ""item"" : ""850 W Addison St""
          }, {
            ""count"" : 1,
            ""item"" : ""937 N Wood St""
          }, {
            ""count"" : 1,
            ""item"" : ""7808 S Halsted St""
          }, {
            ""count"" : 1,
            ""item"" : ""5400 N Lincoln Ave""
          }, {
            ""count"" : 1,
            ""item"" : ""3510 S Michigan Ave""
          }, {
            ""count"" : 1,
            ""item"" : ""727 E 111th St""
          }, {
            ""count"" : 1,
            ""item"" : ""5151 N Milwaukee Ave""
          }, {
            ""count"" : 1,
            ""item"" : ""1900 W Monterey Ave""
          }, {
            ""count"" : 1,
            ""item"" : ""3120 S Halsted St""
          }, {
            ""count"" : 1,
            ""item"" : ""2150 N California Ave""
          }, {
            ""count"" : 1,
            ""item"" : ""300 E 29th St""
          } ]
        },
        ""format"" : {
        }
      }, {
        ""id"" : 2609803,
        ""name"" : ""CITY"",
        ""dataTypeName"" : ""text"",
        ""fieldName"" : ""city"",
        ""position"" : 3,
        ""renderTypeName"" : ""text"",
        ""tableColumnId"" : 1424830,
        ""width"" : 52,
        ""cachedContents"" : {
          ""non_null"" : 26,
          ""smallest"" : ""Chicago"",
          ""null"" : 0,
          ""largest"" : ""Chicago"",
          ""top"" : [ {
            ""count"" : 26,
            ""item"" : ""Chicago""
          } ]
        },
        ""format"" : {
        }
      }, {
        ""id"" : 2609804,
        ""name"" : ""STATE"",
        ""dataTypeName"" : ""text"",
        ""fieldName"" : ""state"",
        ""position"" : 4,
        ""renderTypeName"" : ""text"",
        ""tableColumnId"" : 1424831,
        ""width"" : 50,
        ""cachedContents"" : {
          ""non_null"" : 26,
          ""smallest"" : ""IL"",
          ""null"" : 0,
          ""largest"" : ""IL"",
          ""top"" : [ {
            ""count"" : 26,
            ""item"" : ""IL""
          } ]
        },
        ""format"" : {
        }
      }, {
        ""id"" : 2609807,
        ""name"" : ""ZIP"",
        ""dataTypeName"" : ""text"",
        ""fieldName"" : ""zip"",
        ""position"" : 5,
        ""renderTypeName"" : ""text"",
        ""tableColumnId"" : 1424834,
        ""width"" : 50,
        ""cachedContents"" : {
          ""non_null"" : 26,
          ""smallest"" : ""60607"",
          ""null"" : 0,
          ""largest"" : ""60653"",
          ""top"" : [ {
            ""count"" : 2,
            ""item"" : ""60616""
          }, {
            ""count"" : 2,
            ""item"" : ""60630""
          }, {
            ""count"" : 1,
            ""item"" : ""60625""
          }, {
            ""count"" : 1,
            ""item"" : ""60618""
          }, {
            ""count"" : 1,
            ""item"" : ""60644""
          }, {
            ""count"" : 1,
            ""item"" : ""60628""
          }, {
            ""count"" : 1,
            ""item"" : ""60626""
          }, {
            ""count"" : 1,
            ""item"" : ""60636""
          }, {
            ""count"" : 1,
            ""item"" : ""60639""
          }, {
            ""count"" : 1,
            ""item"" : ""60647""
          }, {
            ""count"" : 1,
            ""item"" : ""60617""
          }, {
            ""count"" : 1,
            ""item"" : ""60622""
          }, {
            ""count"" : 1,
            ""item"" : ""60608""
          }, {
            ""count"" : 1,
            ""item"" : ""60637""
          }, {
            ""count"" : 1,
            ""item"" : ""60609""
          }, {
            ""count"" : 1,
            ""item"" : ""60623""
          }, {
            ""count"" : 1,
            ""item"" : ""60612""
          }, {
            ""count"" : 1,
            ""item"" : ""60620""
          }, {
            ""count"" : 1,
            ""item"" : ""60643""
          }, {
            ""count"" : 1,
            ""item"" : ""60613""
          } ]
        },
        ""format"" : {
          ""precisionStyle"" : ""standard"",
          ""align"" : ""right""
        }
      }, {
        ""id"" : 2616456,
        ""name"" : ""WEBSITE"",
        ""dataTypeName"" : ""url"",
        ""fieldName"" : ""website"",
        ""position"" : 6,
        ""renderTypeName"" : ""url"",
        ""tableColumnId"" : 1428190,
        ""width"" : 100,
        ""cachedContents"" : {
          ""non_null"" : 26,
          ""smallest"" : {
            ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath""
          },
          ""null"" : 0,
          ""largest"" : {
            ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District25""
          },
          ""top"" : [ {
            ""count"" : 1,
            ""item"" : {
              ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District5""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District17""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District12""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District14""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District19""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District9""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District6""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District8""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District20""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District7""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District13""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District23""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District1""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District24""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District22""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District4""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District18""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District11""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District25""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District21""
            }
          } ]
        },
        ""format"" : {
        },
        ""subColumnTypes"" : [ ""url"", ""description"" ]
      }, {
        ""id"" : 2616525,
        ""name"" : ""PHONE"",
        ""dataTypeName"" : ""phone"",
        ""fieldName"" : ""phone"",
        ""position"" : 7,
        ""renderTypeName"" : ""phone"",
        ""tableColumnId"" : 1428240,
        ""width"" : 117,
        ""cachedContents"" : {
          ""non_null"" : 25,
          ""smallest"" : {
            ""phone_number"" : ""312-742-4410""
          },
          ""null"" : 1,
          ""largest"" : {
            ""phone_number"" : ""312-747-8730""
          },
          ""top"" : [ {
            ""count"" : 1,
            ""item"" : {
              ""phone_number"" : ""312-746-8386""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""phone_number"" : ""312-742-5870""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""phone_number"" : ""312-747-8730""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""phone_number"" : ""312-746-8396""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""phone_number"" : ""312-747-8340""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""phone_number"" : ""312-745-0570""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""phone_number"" : ""312-745-4290""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""phone_number"" : ""312-742-8714""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""phone_number"" : ""312-745-3610""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""phone_number"" : ""312-747-8201""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""phone_number"" : ""312-744-5907""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""phone_number"" : ""312-747-8366""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""phone_number"" : ""312-746-8605""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""phone_number"" : ""312-744-8290""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""phone_number"" : ""312-744-5983""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""phone_number"" : ""312-744-8320""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""phone_number"" : ""312-743-1440""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""phone_number"" : ""312-747-8210""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""phone_number"" : ""312-742-4480""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""phone_number"" : ""312-747-8205""
            }
          } ]
        },
        ""format"" : {
          ""align"" : ""left""
        },
        ""subColumnTypes"" : [ ""phone_number"", ""phone_type"" ]
      }, {
        ""id"" : 2616542,
        ""name"" : ""FAX"",
        ""dataTypeName"" : ""text"",
        ""fieldName"" : ""fax"",
        ""position"" : 8,
        ""renderTypeName"" : ""text"",
        ""tableColumnId"" : 1428248,
        ""width"" : 100,
        ""cachedContents"" : {
          ""non_null"" : 25,
          ""smallest"" : ""312-742-4421"",
          ""null"" : 1,
          ""largest"" : ""312-747-8545"",
          ""top"" : [ {
            ""count"" : 1,
            ""item"" : ""312-747-5329""
          }, {
            ""count"" : 1,
            ""item"" : ""312-742-8803""
          }, {
            ""count"" : 1,
            ""item"" : ""312-746-4281""
          }, {
            ""count"" : 1,
            ""item"" : ""312-746-4235""
          }, {
            ""count"" : 1,
            ""item"" : ""312-742-5411""
          }, {
            ""count"" : 1,
            ""item"" : ""312-746-4353""
          }, {
            ""count"" : 1,
            ""item"" : ""312-747-6558""
          }, {
            ""count"" : 1,
            ""item"" : ""312-745-3649""
          }, {
            ""count"" : 1,
            ""item"" : ""312-745-3694""
          }, {
            ""count"" : 1,
            ""item"" : ""312-745-0814""
          }, {
            ""count"" : 1,
            ""item"" : ""312-747-5396""
          }, {
            ""count"" : 1,
            ""item"" : ""312-747-5276""
          }, {
            ""count"" : 1,
            ""item"" : ""312-742-5771""
          }, {
            ""count"" : 1,
            ""item"" : ""312-747-7429""
          }, {
            ""count"" : 1,
            ""item"" : ""312-747-5919""
          }, {
            ""count"" : 1,
            ""item"" : ""312-744-2422""
          }, {
            ""count"" : 1,
            ""item"" : ""312-744-4437""
          }, {
            ""count"" : 1,
            ""item"" : ""312-744-6928""
          }, {
            ""count"" : 1,
            ""item"" : ""312-747-5935""
          }, {
            ""count"" : 1,
            ""item"" : ""312-747-8545""
          } ]
        },
        ""format"" : {
        }
      }, {
        ""id"" : 2616544,
        ""name"" : ""TTY"",
        ""dataTypeName"" : ""text"",
        ""fieldName"" : ""tty"",
        ""position"" : 9,
        ""renderTypeName"" : ""text"",
        ""tableColumnId"" : 1428249,
        ""width"" : 100,
        ""cachedContents"" : {
          ""non_null"" : 25,
          ""smallest"" : ""312-742-4423"",
          ""null"" : 1,
          ""largest"" : ""312-747-9172"",
          ""top"" : [ {
            ""count"" : 1,
            ""item"" : ""312-745-3693""
          }, {
            ""count"" : 1,
            ""item"" : ""312-744-8260""
          }, {
            ""count"" : 1,
            ""item"" : ""312-743-1485""
          }, {
            ""count"" : 1,
            ""item"" : ""312-746-9868""
          }, {
            ""count"" : 1,
            ""item"" : ""312-747-6652""
          }, {
            ""count"" : 1,
            ""item"" : ""312-746-8383""
          }, {
            ""count"" : 1,
            ""item"" : ""312-745-0569""
          }, {
            ""count"" : 1,
            ""item"" : ""312-747-9170""
          }, {
            ""count"" : 1,
            ""item"" : ""312-746-5151""
          }, {
            ""count"" : 1,
            ""item"" : ""312-747-6655""
          }, {
            ""count"" : 1,
            ""item"" : ""312-747-8116""
          }, {
            ""count"" : 1,
            ""item"" : ""312-746-5999""
          }, {
            ""count"" : 1,
            ""item"" : ""312-747-9169""
          }, {
            ""count"" : 1,
            ""item"" : ""312-742-5773""
          }, {
            ""count"" : 1,
            ""item"" : ""312-747-7471""
          }, {
            ""count"" : 1,
            ""item"" : ""312-747-9172""
          }, {
            ""count"" : 1,
            ""item"" : ""312-742-8841""
          }, {
            ""count"" : 1,
            ""item"" : ""312-747-9168""
          }, {
            ""count"" : 1,
            ""item"" : ""312-744-7603""
          }, {
            ""count"" : 1,
            ""item"" : ""312-744-9872""
          } ]
        },
        ""format"" : {
        }
      }, {
        ""id"" : 2616479,
        ""name"" : ""LOCATION"",
        ""dataTypeName"" : ""location"",
        ""fieldName"" : ""location"",
        ""position"" : 10,
        ""renderTypeName"" : ""location"",
        ""tableColumnId"" : 1428203,
        ""width"" : 100,
        ""cachedContents"" : {
          ""non_null"" : 26,
          ""smallest"" : {
            ""longitude"" : -87.65715796324291,
            ""latitude"" : 41.88031148430877,
            ""human_address"" : ""{\""address\"":\""100 S Racine Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60607\""}""
          },
          ""null"" : 0,
          ""largest"" : {
            ""longitude"" : -87.67202575687757,
            ""latitude"" : 41.8988188771907,
            ""human_address"" : ""{\""address\"":\""937 N Wood St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60622\""}""
          },
          ""top"" : [ {
            ""count"" : 1,
            ""item"" : {
              ""longitude"" : -87.62003595036374,
              ""latitude"" : 41.84226950798831,
              ""human_address"" : ""{\""address\"":\""300 E 29th St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60616\""}""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""longitude"" : -87.65715796324291,
              ""latitude"" : 41.88031148430877,
              ""human_address"" : ""{\""address\"":\""100 S Racine Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60607\""}""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""longitude"" : -87.64947786133158,
              ""latitude"" : 41.94665927496925,
              ""human_address"" : ""{\""address\"":\""3600 N Halsted St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60613\""}""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""longitude"" : -87.65945323490796,
              ""latitude"" : 41.77965870830844,
              ""human_address"" : ""{\""address\"":\""1438 W 63rd St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60636\""}""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""longitude"" : -87.6895222785126,
              ""latitude"" : 41.93948016174912,
              ""human_address"" : ""{\""address\"":\""2452 W Belmont Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60618\""}""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""longitude"" : -87.70519897133927,
              ""latitude"" : 41.873690811457,
              ""human_address"" : ""{\""address\"":\""3151 W Harrison St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60612\""}""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""longitude"" : -87.67118638353655,
              ""latitude"" : 41.99944797184307,
              ""human_address"" : ""{\""address\"":\""6464 N Clark St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60626\""}""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""longitude"" : -87.6056617928308,
              ""latitude"" : 41.76697112291843,
              ""human_address"" : ""{\""address\"":\""7040 S Cottage Grove Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60637\""}""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""longitude"" : -87.64661990954984,
              ""latitude"" : 41.83750310123302,
              ""human_address"" : ""{\""address\"":\""3120 S Halsted St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60608\""}""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""longitude"" : -87.66855780581959,
              ""latitude"" : 41.691350048962875,
              ""human_address"" : ""{\""address\"":\""1900 W Monterey Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60643\""}""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""longitude"" : -87.64353523498934,
              ""latitude"" : 41.90311763649074,
              ""human_address"" : ""{\""address\"":\""1160 N Larrabee St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60610\""}""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""longitude"" : -87.64414141672431,
              ""latitude"" : 41.7522817574851,
              ""human_address"" : ""{\""address\"":\""7808 S Halsted St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60620\""}""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""longitude"" : -87.76623800785116,
              ""latitude"" : 41.974188798165315,
              ""human_address"" : ""{\""address\"":\""5151 N Milwaukee Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60630\""}""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""longitude"" : -87.62330200626332,
              ""latitude"" : 41.83086072588734,
              ""human_address"" : ""{\""address\"":\""3510 S Michigan Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60653\""}""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""longitude"" : -87.70840333296276,
              ""latitude"" : 41.85636188418744,
              ""human_address"" : ""{\""address\"":\""3315 W Ogden Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60623\""}""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""longitude"" : -87.56718279470178,
              ""latitude"" : 41.70778489172678,
              ""human_address"" : ""{\""address\"":\""2255 E 103rd St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60617\""}""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""longitude"" : -87.76465420759772,
              ""latitude"" : 41.91848587824693,
              ""human_address"" : ""{\""address\"":\""5555 W Grand Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60639\""}""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""longitude"" : -87.69301593304144,
              ""latitude"" : 41.97954202254066,
              ""human_address"" : ""{\""address\"":\""5400 N Lincoln Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60625\""}""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""longitude"" : -87.630663656445,
              ""latitude"" : 41.8017738753714,
              ""human_address"" : ""{\""address\"":\""5101 S Wentworth Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60609\""}""
            }
          }, {
            ""count"" : 1,
            ""item"" : {
              ""longitude"" : -87.69732502896069,
              ""latitude"" : 41.92125066466082,
              ""human_address"" : ""{\""address\"":\""2150 N California Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60647\""}""
            }
          } ]
        },
        ""format"" : {
        },
        ""subColumnTypes"" : [ ""human_address"", ""latitude"", ""longitude"", ""machine_address"", ""needs_recoding"" ]
      } ],
      ""grants"" : [ {
        ""inherited"" : false,
        ""type"" : ""viewer"",
        ""flags"" : [ ""public"" ]
      } ],
      ""metadata"" : {
        ""custom_fields"" : {
          ""Metadata"" : {
            ""Time Period"" : ""Current as of 2011"",
            ""Data Owner"" : ""Police"",
            ""Frequency"" : ""Data is updated as needed""
          }
        },
        ""rdfSubject"" : ""0"",
        ""attachments"" : [ {
          ""blobId"" : ""DB725B05-F930-4CCC-8AEE-3175DB0B10EA"",
          ""name"" : ""police_stations_poly.zip"",
          ""filename"" : ""police_stations_poly.zip""
        } ],
        ""rowIdentifier"" : ""0"",
        ""rdfClass"" : """"
      },
      ""owner"" : {
        ""id"" : ""scy9-9wg4"",
        ""displayName"" : ""cocadmin"",
        ""emailUnsubscribed"" : false,
        ""privacyControl"" : ""login"",
        ""profileImageUrlLarge"" : ""/images/profile/4783/7574/CitySeal_Small_large.jpg"",
        ""profileImageUrlMedium"" : ""/images/profile/4783/7574/CitySeal_Small_thumb.jpg"",
        ""profileImageUrlSmall"" : ""/images/profile/4783/7574/CitySeal_Small_tiny.jpg"",
        ""profileLastModified"" : 1320762424,
        ""roleName"" : ""administrator"",
        ""screenName"" : ""cocadmin"",
        ""rights"" : [ ""create_datasets"", ""edit_others_datasets"", ""edit_sdp"", ""edit_site_theme"", ""moderate_comments"", ""manage_users"", ""chown_datasets"", ""edit_nominations"", ""approve_nominations"", ""feature_items"", ""federations"", ""manage_stories"", ""manage_approval"", ""change_configurations"", ""view_domain"", ""view_others_datasets"" ]
      },
      ""query"" : {
      },
      ""rights"" : [ ""read"" ],
      ""tableAuthor"" : {
        ""id"" : ""scy9-9wg4"",
        ""displayName"" : ""cocadmin"",
        ""emailUnsubscribed"" : false,
        ""privacyControl"" : ""login"",
        ""profileImageUrlLarge"" : ""/images/profile/4783/7574/CitySeal_Small_large.jpg"",
        ""profileImageUrlMedium"" : ""/images/profile/4783/7574/CitySeal_Small_thumb.jpg"",
        ""profileImageUrlSmall"" : ""/images/profile/4783/7574/CitySeal_Small_tiny.jpg"",
        ""profileLastModified"" : 1320762424,
        ""roleName"" : ""administrator"",
        ""screenName"" : ""cocadmin"",
        ""rights"" : [ ""create_datasets"", ""edit_others_datasets"", ""edit_sdp"", ""edit_site_theme"", ""moderate_comments"", ""manage_users"", ""chown_datasets"", ""edit_nominations"", ""approve_nominations"", ""feature_items"", ""federations"", ""manage_stories"", ""manage_approval"", ""change_configurations"", ""view_domain"", ""view_others_datasets"" ]
      },
      ""tags"" : [ ""facilities"", ""gis"" ],
      ""flags"" : [ ""default"" ]
    }
  },
  ""entries"" : [ {
    ""position"" : 1,
    ""sid"" : 1,
    ""DISTRICT"" : ""Headquarters"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""3510 S Michigan Ave"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath""
    },
    ""PHONE"" : {
    },
    ""id"" : ""8518106A-32BD-4BAC-885D-96B7FDB34F34"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.62330200626332"",
      ""latitude"" : ""41.83086072588734"",
      ""human_address"" : ""{\""address\"":\""3510 S Michigan Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60653\""}""
    },
    ""updated_at"" : 1294260820,
    ""ZIP"" : ""60653"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""CITY"" : ""Chicago""
  }
, {
    ""position"" : 2,
    ""sid"" : 2,
    ""DISTRICT"" : ""1"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""1718 S State St"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District1""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-745-4290""
    },
    ""id"" : ""0FE31F03-2C78-46A1-BA7E-609DA3020BAB"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.62734256712464"",
      ""latitude"" : ""41.85882087432612"",
      ""human_address"" : ""{\""address\"":\""1718 S State St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60616\""}""
    },
    ""updated_at"" : 1294263787,
    ""ZIP"" : ""60616"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-745-3693"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-745-3694""
  }
, {
    ""position"" : 3,
    ""sid"" : 3,
    ""DISTRICT"" : ""2"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""5101 S Wentworth Ave"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District2""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-747-8366""
    },
    ""id"" : ""FA1E611F-E3E2-4214-AB93-E9A2038FEBA4"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.630663656445"",
      ""latitude"" : ""41.8017738753714"",
      ""human_address"" : ""{\""address\"":\""5101 S Wentworth Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60609\""}""
    },
    ""updated_at"" : 1294263759,
    ""ZIP"" : ""60609"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-747-6656"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-747-5396""
  }
, {
    ""position"" : 4,
    ""sid"" : 4,
    ""DISTRICT"" : ""3"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""7040 S Cottage Grove Ave"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District3""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-747-8201""
    },
    ""id"" : ""4A4F311D-DB63-4971-87CA-98053B7001B6"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.6056617928308"",
      ""latitude"" : ""41.76697112291843"",
      ""human_address"" : ""{\""address\"":\""7040 S Cottage Grove Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60637\""}""
    },
    ""updated_at"" : 1294263737,
    ""ZIP"" : ""60637"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-747-9168"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-747-5479""
  }
, {
    ""position"" : 5,
    ""sid"" : 5,
    ""DISTRICT"" : ""4"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""2255 E 103rd St"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District4""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-747-8205""
    },
    ""id"" : ""32F09548-7A6E-462C-86F1-78DE8AD333BA"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.56718279470178"",
      ""latitude"" : ""41.70778489172678"",
      ""human_address"" : ""{\""address\"":\""2255 E 103rd St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60617\""}""
    },
    ""updated_at"" : 1294263718,
    ""ZIP"" : ""60617"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-747-9169"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-747-5276""
  }
, {
    ""position"" : 6,
    ""sid"" : 6,
    ""DISTRICT"" : ""5"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""727 E 111th St"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District5""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-747-8210""
    },
    ""id"" : ""1E67F8FC-0838-4DFA-942C-12C0DC9D862D"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.60536435875626"",
      ""latitude"" : ""41.69279473591493"",
      ""human_address"" : ""{\""address\"":\""727 E 111th St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60628\""}""
    },
    ""updated_at"" : 1294263700,
    ""ZIP"" : ""60628"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-747-9170"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-747-5935""
  }
, {
    ""position"" : 7,
    ""sid"" : 7,
    ""DISTRICT"" : ""6"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""7808 S Halsted St"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District6""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-745-3610""
    },
    ""id"" : ""40CAE5DD-FAEB-4A1A-AD0A-AC77DD00D6ED"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.64414141672431"",
      ""latitude"" : ""41.7522817574851"",
      ""human_address"" : ""{\""address\"":\""7808 S Halsted St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60620\""}""
    },
    ""updated_at"" : 1294263680,
    ""ZIP"" : ""60620"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-745-3639"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-745-3649""
  }
, {
    ""position"" : 8,
    ""sid"" : 8,
    ""DISTRICT"" : ""7"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""1438 W 63rd St"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District7""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-747-8220""
    },
    ""id"" : ""398DE9E3-1D18-4E57-A4D8-266C2EAFBB35"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.65945323490796"",
      ""latitude"" : ""41.77965870830844"",
      ""human_address"" : ""{\""address\"":\""1438 W 63rd St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60636\""}""
    },
    ""updated_at"" : 1294263661,
    ""ZIP"" : ""60636"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-747-6652"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-747-6558""
  }
, {
    ""position"" : 9,
    ""sid"" : 9,
    ""DISTRICT"" : ""8"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""3420 W 63rd St"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District8""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-747-8730""
    },
    ""id"" : ""28CF2F81-9340-4AC2-AECD-38F67BEE89B2"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.70878104821506"",
      ""latitude"" : ""41.77899321000677"",
      ""human_address"" : ""{\""address\"":\""3420 W 63rd St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60629\""}""
    },
    ""updated_at"" : 1294263643,
    ""ZIP"" : ""60629"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-747-8116"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-747-8545""
  }
, {
    ""position"" : 10,
    ""sid"" : 10,
    ""DISTRICT"" : ""9"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""3120 S Halsted St"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District9""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-747-8227""
    },
    ""id"" : ""A85194EE-7DD4-475F-9A11-C0524AD6227A"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.64661990954984"",
      ""latitude"" : ""41.83750310123302"",
      ""human_address"" : ""{\""address\"":\""3120 S Halsted St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60608\""}""
    },
    ""updated_at"" : 1294263626,
    ""ZIP"" : ""60608"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-747-9172"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-747-5329""
  }
, {
    ""position"" : 11,
    ""sid"" : 11,
    ""DISTRICT"" : ""10"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""3315 W Ogden Ave"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District10""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-747-7511""
    },
    ""id"" : ""DEC786A2-F405-41A2-973E-ABD4C236120B"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.70840333296276"",
      ""latitude"" : ""41.85636188418744"",
      ""human_address"" : ""{\""address\"":\""3315 W Ogden Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60623\""}""
    },
    ""updated_at"" : 1294263609,
    ""ZIP"" : ""60623"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-747-7471"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-747-7429""
  }
, {
    ""position"" : 12,
    ""sid"" : 12,
    ""DISTRICT"" : ""11"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""3151 W Harrison St"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District11""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-746-8386""
    },
    ""id"" : ""B2F9A0B6-2D41-4630-86EA-4F2DA5F4DB1D"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.70519897133927"",
      ""latitude"" : ""41.873690811457"",
      ""human_address"" : ""{\""address\"":\""3151 W Harrison St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60612\""}""
    },
    ""updated_at"" : 1294263592,
    ""ZIP"" : ""60612"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-746-5151"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-746-4281""
  }
, {
    ""position"" : 13,
    ""sid"" : 13,
    ""DISTRICT"" : ""12"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""100 S Racine Ave"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District12""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-746-8396""
    },
    ""id"" : ""F4078973-35B5-4994-AA4A-535B43185486"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.65715796324291"",
      ""latitude"" : ""41.88031148430877"",
      ""human_address"" : ""{\""address\"":\""100 S Racine Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60607\""}""
    },
    ""updated_at"" : 1294263574,
    ""ZIP"" : ""60607"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-746-9868"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-746-4248""
  }
, {
    ""position"" : 14,
    ""sid"" : 14,
    ""DISTRICT"" : ""13"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""937 N Wood St"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District13""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-746-8350""
    },
    ""id"" : ""6ACBC934-FEFF-495E-98A9-DBF3D5CC9AFF"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.67202575687757"",
      ""latitude"" : ""41.8988188771907"",
      ""human_address"" : ""{\""address\"":\""937 N Wood St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60622\""}""
    },
    ""updated_at"" : 1294263553,
    ""ZIP"" : ""60622"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-746-5999"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-746-4235""
  }
, {
    ""position"" : 15,
    ""sid"" : 15,
    ""DISTRICT"" : ""14"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""2150 N California Ave"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District14""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-744-8290""
    },
    ""id"" : ""7F0B1FDC-A9E2-4052-AF45-FA550F8BF23A"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.69732502896069"",
      ""latitude"" : ""41.92125066466082"",
      ""human_address"" : ""{\""address\"":\""2150 N California Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60647\""}""
    },
    ""updated_at"" : 1294263532,
    ""ZIP"" : ""60647"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-744-8260"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-744-2422""
  }
, {
    ""position"" : 16,
    ""sid"" : 16,
    ""DISTRICT"" : ""15"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""5701 W Madison St"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District15""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-743-1440""
    },
    ""id"" : ""257F40E6-2324-44DA-92FA-B1ADEA51DB2D"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.76818126825033"",
      ""latitude"" : ""41.87978022983041"",
      ""human_address"" : ""{\""address\"":\""5701 W Madison St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60644\""}""
    },
    ""updated_at"" : 1294263515,
    ""ZIP"" : ""60644"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-743-1485"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-743-1366""
  }
, {
    ""position"" : 17,
    ""sid"" : 17,
    ""DISTRICT"" : ""16"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""5151 N Milwaukee Ave"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District16""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-742-4480""
    },
    ""id"" : ""C27E321B-F628-4C79-86C7-D1B883E99568"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.76623800785116"",
      ""latitude"" : ""41.974188798165315"",
      ""human_address"" : ""{\""address\"":\""5151 N Milwaukee Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60630\""}""
    },
    ""updated_at"" : 1294263495,
    ""ZIP"" : ""60630"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-742-4423"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-742-4421""
  }
, {
    ""position"" : 18,
    ""sid"" : 18,
    ""DISTRICT"" : ""17"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""4650 N Pulaski Rd"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District17""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-742-4410""
    },
    ""id"" : ""36EF271C-656B-4824-BD06-BD214B7F6A43"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.72816143794648"",
      ""latitude"" : ""41.966052192101166"",
      ""human_address"" : ""{\""address\"":\""4650 N Pulaski Rd\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60630\""}""
    },
    ""updated_at"" : 1294263472,
    ""ZIP"" : ""60630"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-742-5451"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-742-5411""
  }
, {
    ""position"" : 19,
    ""sid"" : 19,
    ""DISTRICT"" : ""18"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""1160 N Larrabee St"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District18""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-742-5870""
    },
    ""id"" : ""DAFF4553-A73E-4F3D-B6A6-D24714E919F4"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.64353523498934"",
      ""latitude"" : ""41.90311763649074"",
      ""human_address"" : ""{\""address\"":\""1160 N Larrabee St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60610\""}""
    },
    ""updated_at"" : 1294263455,
    ""ZIP"" : ""60610"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-742-5773"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-742-5771""
  }
, {
    ""position"" : 20,
    ""sid"" : 20,
    ""DISTRICT"" : ""19"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""2452 W Belmont Ave"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District19""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-744-5983""
    },
    ""id"" : ""72E37562-D949-469B-B5F4-563663571C5A"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.6895222785126"",
      ""latitude"" : ""41.93948016174912"",
      ""human_address"" : ""{\""address\"":\""2452 W Belmont Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60618\""}""
    },
    ""updated_at"" : 1294263439,
    ""ZIP"" : ""60618"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-744-9872"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-744-4437""
  }
, {
    ""position"" : 21,
    ""sid"" : 21,
    ""DISTRICT"" : ""20"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""5400 N Lincoln Ave"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District20""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-742-8714""
    },
    ""id"" : ""62303A50-B037-4B66-BE35-96780CDECF8C"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.69301593304144"",
      ""latitude"" : ""41.97954202254066"",
      ""human_address"" : ""{\""address\"":\""5400 N Lincoln Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60625\""}""
    },
    ""updated_at"" : 1294263423,
    ""ZIP"" : ""60625"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-742-8841"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-742-8803""
  }
, {
    ""position"" : 22,
    ""sid"" : 22,
    ""DISTRICT"" : ""21"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""300 E 29th St"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District21""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-747-8340""
    },
    ""id"" : ""3E813253-CA28-4675-BD5E-16F266242E67"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.62003595036374"",
      ""latitude"" : ""41.84226950798831"",
      ""human_address"" : ""{\""address\"":\""300 E 29th St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60616\""}""
    },
    ""updated_at"" : 1294263404,
    ""ZIP"" : ""60616"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-747-6655"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-747-5919""
  }
, {
    ""position"" : 23,
    ""sid"" : 23,
    ""DISTRICT"" : ""22"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""1900 W Monterey Ave"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District22""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-745-0570""
    },
    ""id"" : ""4607A0F7-41A4-456F-B456-F820E7481A96"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.66855780581959"",
      ""latitude"" : ""41.691350048962875"",
      ""human_address"" : ""{\""address\"":\""1900 W Monterey Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60643\""}""
    },
    ""updated_at"" : 1294263387,
    ""ZIP"" : ""60643"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-745-0569"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-745-0814""
  }
, {
    ""position"" : 24,
    ""sid"" : 24,
    ""DISTRICT"" : ""23"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""850 W Addison St"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District23""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-744-8320""
    },
    ""id"" : ""6FFB5725-A09D-4F7C-9F90-A2DC8D7C33D1"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.64947786133158"",
      ""latitude"" : ""41.94665927496925"",
      ""human_address"" : ""{\""address\"":\""3600 N Halsted St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60613\""}""
    },
    ""updated_at"" : 1301324311,
    ""ZIP"" : ""60613"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-744-8011"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-744-4481""
  }
, {
    ""position"" : 25,
    ""sid"" : 25,
    ""DISTRICT"" : ""24"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""6464 N Clark St"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District24""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-744-5907""
    },
    ""id"" : ""B0C08223-F374-4039-904B-EE30B4AE1F88"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.67118638353655"",
      ""latitude"" : ""41.99944797184307"",
      ""human_address"" : ""{\""address\"":\""6464 N Clark St\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60626\""}""
    },
    ""updated_at"" : 1294263345,
    ""ZIP"" : ""60626"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-744-7603"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-744-6928""
  }
, {
    ""position"" : 26,
    ""sid"" : 26,
    ""DISTRICT"" : ""25"",
    ""STATE"" : ""IL"",
    ""ADDRESS"" : ""5555 W Grand Ave"",
    ""updated_meta"" : ""386464"",
    ""WEBSITE"" : {
      ""url"" : ""https://portal.chicagopolice.org/portal/page/portal/ClearPath/Communities/Districts/District25""
    },
    ""PHONE"" : {
      ""phone_number"" : ""312-746-8605""
    },
    ""id"" : ""620E8058-C5ED-4D1F-81F9-FAFD04CC5143"",
    ""LOCATION"" : {
      ""needs_recoding"" : false,
      ""longitude"" : ""-87.76465420759772"",
      ""latitude"" : ""41.91848587824693"",
      ""human_address"" : ""{\""address\"":\""5555 W Grand Ave\"",\""city\"":\""Chicago\"",\""state\"":\""IL\"",\""zip\"":\""60639\""}""
    },
    ""updated_at"" : 1294262912,
    ""ZIP"" : ""60639"",
    ""created_meta"" : ""386464"",
    ""created_at"" : 1294260724,
    ""TTY"" : ""312-746-8383"",
    ""CITY"" : ""Chicago"",
    ""FAX"" : ""312-746-4353""
  }
 ]
}";
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            var stream = new MemoryStream();
            var testMessage = new TestMessage() { Stream = stream };

            var converter = new SodaToODataConverter();
            using (var writer = new ODataMessageWriter(testMessage))
            {
                converter.ConvertToFeed(writer.CreateODataFeedWriter(), "http://data.cityofchicago.org/views/z8bn-74gv", JsonText);
            }

            var text = Encoding.UTF8.GetString(stream.ToArray());
            var xml = XElement.Parse(text);
            Console.WriteLine(xml.ToString());
        }

        private class TestMessage : IODataResponseMessage
        {
            private readonly Dictionary<string, string> headers = new Dictionary<string, string>();

            public Stream Stream { get; set; }

            public string GetHeader(string headerName)
            {
                string headerValue;
                if (!this.headers.TryGetValue(headerName, out headerValue))
                {
                    headerValue = null;
                }

                return headerValue;
            }

            public Stream GetStream()
            {
                return this.Stream;
            }

            public IEnumerable<KeyValuePair<string, string>> Headers
            {
                get { return this.headers; }
            }

            public void SetHeader(string headerName, string headerValue)
            {
                this.headers[headerName] = headerValue;
            }

            public int StatusCode
            {
                get;
                set;
            }
        }
    }
}
            