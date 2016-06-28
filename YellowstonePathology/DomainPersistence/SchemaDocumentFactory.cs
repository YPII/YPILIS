using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using System.Reflection;

namespace YellowstonePathology.Business.Domain.Persistence.Schema
{
    public class SchemaDocumentFactory
    {
		public static XDocument GetSchemaDocument(Type type)
		{
			XDocument schemaDocument = null;
			string itemName = type.Name;
			switch (itemName)
			{
				case "Lock":
				case "LockItem":
					schemaDocument = Schema.LockSchema.Instance;
					break;
				case "ClientOrder":
				case "ClientServicesLog":
				case "ClientOrderDetail":
					schemaDocument = Schema.ClientOrderSchema.Instance;
					break;
				case "BarcodeScan":
				case "BlockSession":
				case "OrderComment":
				//case "UserPreference":
				case "SystemUser":
				case "SystemUserRole":
				case "SystemRole":
				case "DnaComment":
				case "DnaInterpretation":
				case "DnaReference":
				case "DnaResult":
				case "HpvRequisitionInstruction":
				case "ScreeningImpression":
				case "SpecimenAdequacy":
				case "OtherCondition":
				case "CytologyReportComment":
				case "SpecimenAdequacyComment":
				case "PanelSet":
				case "OrderableTestCptCode":
				case "OrderableTestIcd9Code":
				case "CommentListItem":
				case "CptCode":
				case "StainTest":
				case "ImmunoComment":
				case "Test":
				case "ResultItem":
				case "MaterialLocation":
					schemaDocument = Schema.LocalDataSchema.Instance;
					break;
				case "SlideOrder":
				case "AccessionSlideOrderView":
					schemaDocument = Schema.AccessionSlideOrderSchema.Instance;
					break;
				/*case "AccessionOrder":
				case "SpecimenOrder":
				case "AliquotOrder":
				case "PanelSetOrder":
				case "PanelOrder":
				case "TestOrder":
				case "PanelSetOrderComment":
				case "SurgicalResultItem":
				case "SurgicalSpecimenResultItem":
				case "StainResultItem":
				case "IntraoperativeConsultationResultItem":
				case "ICD9BillingCode":				
				case "SurgicalBillingItem":
				case "SurgicalResultAuditItem":
				case "SurgicalSpecimenResultAuditItem":
				case "Her2Result":
				case "FishResultItem":
				case "ReportDistributionItemV2":
				case "ReportDistributionLogItemV2":
				case "AmendmentItem":
				case "PanelOrderBatch":
				case "CaseDocument":
				case "AliquotOrderListItem":
				case "TestSpecimenOrderItem":
				case "TestOrderListItem":				
				case "ReportNo":
				case "RecentAccessionView":
				case "CptBillingCode":
				case "CptBillingCodeLog":
				case "AutopsyResult":				
					schemaDocument = Schema.AccessionOrderSchema.Instance;
					break;*/
				case "ErPrSemiQuantitativeResult":
					schemaDocument = Schema.ErPrSemiQuantitativeResultSchema.Instance;
					break;
				case "SpecimenLogAccessionOrder":
					schemaDocument = Schema.SpecimenLogAccessionOrderSchema.Instance;
					break;
				case "SlideLabel":
					schemaDocument = Schema.SlideLabelSchema.Instance;
					break;
				case "Client":
				case "ClientLocation":
					schemaDocument = Schema.ClientSchema.Instance;
					break;
				case "Physician":
					schemaDocument = Schema.PhysicianSchema.Instance;
					break;
				case "PhysicianClient":
					schemaDocument = Schema.PhysicianClientSchema.Instance;
					break;
				case "BillingCptCodeDetail":
				case "BillingCptCode":
				case "BillingIcd9Code":                
				case "BillingSpecimen":
				case "BillingReport":
				case "BillingAccession":
                case "SvhIncomingBillingData":
					schemaDocument = Schema.BillingSchema.Instance;
					break;
				case "OrderCommentLog":
				case "Temp":
					schemaDocument = Schema.LabEventLogSchema.Instance;
					break;
				case "ChannelMessage":
					schemaDocument = Schema.ChannelMessageSchema.Instance;
					break;
				case "PathologistSearchResult":
					schemaDocument = Schema.SearchSchema.Instance;
					break;
				case "AccessionSummary":
					schemaDocument = Schema.AccessionSummarySchema.Instance;
					break;
				//case "FlowLogListItem":
				case "LeukemiaLymphomaItem":
				case "FlowMarkerItem":
					schemaDocument = Schema.FlowSchema.Instance;
					break;
				case "SlideTrackingLog":
				case "MaterialTrackingBatch":
					schemaDocument = Schema.SlideTrackingSchema.Instance;
					break;
				case "WebServiceAccount":
				case "WebServiceAccountClient":
					schemaDocument = Schema.WebServiceAccountSchema.Instance;
					break;
				case "FlowAccession":
				case "FlowComment":
				case "FlowLeukemia":
				case "FlowMarker":
				case "Marker":
					schemaDocument = Schema.FlowContractSchema.Instance;
					break;
				case "Shipment":
					schemaDocument = Schema.ShipmentSchema.Instance;
					break;
				case "ClientOrderMedia":
					schemaDocument = Schema.ClientOrderMediaSchema.Instance;
					break;
				case "PlacentalPathologyQuestionnaire":
					schemaDocument = Schema.PlacentalPathologyQuestionnaire.Instance;
					break;
				case "BillingRule":
					schemaDocument = Schema.BillingRuleSchema.Instance;
					break;
				case "BillingRuleSet":
					schemaDocument = Schema.BillingRuleSetSchema.Instance;
					break;
				case "CptCodeGroup":
					schemaDocument = Schema.CptCodeGroupSchema.Instance;
					break;
				case "CptCodeGroupCode":
					schemaDocument = Schema.CptCodeGroupCodeSchema.Instance;
					break;
				case "OrderCategory":
					schemaDocument = Schema.OrderCategorySchema.Instance;
					break;
				case "OrderType":
					schemaDocument = Schema.OrderTypeSchema.Instance;
					break;
				case "ClientOrderCytologyProperty":
					schemaDocument = Schema.ClientOrderCytologyPropertySchema.Instance;
					break;
				case "ClientOrderFNAProperty":
					schemaDocument = Schema.ClientOrderFNAPropertySchema.Instance;
					break;
				case "ClientOrderDetailFNAProperty":
					schemaDocument = Schema.ClientOrderDetailFNAPropertySchema.Instance;
					break;
				case "Facility":
					schemaDocument = Schema.FacilitySchema.Instance;
					break;
				case "ClientOrderDetailSurgicalProperty":
					schemaDocument = Schema.ClientOrderDetailSurgicalPropertySchema.Instance;
					break;
                case "ReportDistributionItem":
                    schemaDocument = Schema.AccessionOrder.ReportDistributionSchema.Instance;
                    break;
                case "ReportDistributionLogItem":
                    schemaDocument = Schema.AccessionOrder.ReportDistributionLogSchema.Instance;
                    break;
				default:
					throw new NotImplementedException(itemName + " is not found in the SchemaDocumentFactory.");
			}			
			return schemaDocument;
		}

		private static XDocument SpecificType(XDocument sourceSchema, string itemName)
		{            
			XDocument specificSchema = new XDocument(sourceSchema);
			specificSchema.Root.RemoveNodes();
			string typeName = itemName + "Type";
			List<XElement> elements = sourceSchema.Root.Elements().ToList<XElement>();
			foreach (XElement e in elements)
			{
				XAttribute nameAttribute = e.Attribute("name");
				if (nameAttribute != null)
				{
					string name = nameAttribute.Value;
					if (name == itemName)
					{
						specificSchema.Root.Add(e);
						break;
					}
				}
			}
			foreach (XElement e in elements)
			{
				XAttribute nameAttribute = e.Attribute("name");
				if (nameAttribute != null)
				{
					string name = nameAttribute.Value;
					if (name == typeName)
					{
						specificSchema.Root.Add(e);
						break;
					}
				}
			}
			return specificSchema;
		}
	}
}
