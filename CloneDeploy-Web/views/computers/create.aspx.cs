﻿using System;
using CloneDeploy_Common;
using CloneDeploy_Entities;
using CloneDeploy_Web.BasePages;

namespace CloneDeploy_Web.views.computers
{
    public partial class Addcomputers : Computers
    {
        protected void ButtonAddComputer_Click(object sender, EventArgs e)
        {
            RequiresAuthorization(AuthorizationStrings.CreateComputer);
            var computer = new ComputerEntity
            {
                Name = txtComputerName.Text,
                Mac = txtComputerMac.Text,
                ImageId = Convert.ToInt32(ddlComputerImage.SelectedValue),
                ImageProfileId =
                    Convert.ToInt32(ddlComputerImage.SelectedValue) == -1
                        ? -1
                        : Convert.ToInt32(ddlImageProfile.SelectedValue),
                Description = txtComputerDesc.Text,
                SiteId = Convert.ToInt32(ddlSite.SelectedValue),
                BuildingId = Convert.ToInt32(ddlBuilding.SelectedValue),
                RoomId = Convert.ToInt32(ddlRoom.SelectedValue),
                ClusterGroupId = Convert.ToInt32(ddlClusterGroup.SelectedValue),
                CustomAttribute1 = txtCustom1.Text,
                CustomAttribute2 = txtCustom2.Text,
                CustomAttribute3 = txtCustom3.Text,
                CustomAttribute4 = txtCustom4.Text,
                CustomAttribute5 = txtCustom5.Text,
                AlternateServerIpId = Convert.ToInt32(altServerIp.SelectedValue)
            };

            var result = Call.ComputerApi.Post(computer);

            if (!result.Success)
                EndUserMessage = result.ErrorMessage;
            else
            {
                Call.ComputerApi.AddToSmartGroups(computer);
                EndUserMessage = "Successfully Created Computer";
                if (!createAnother.Checked)
                    Response.Redirect(string.Format("~/views/computers/edit.aspx?computerid={0}", result.Id));
            }
        }

        protected void ddlComputerImage_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateImageProfilesDdl(ddlImageProfile, Convert.ToInt32(ddlComputerImage.SelectedValue));
            try
            {
                ddlImageProfile.SelectedIndex = 1;
            }
            catch
            {
                //ignore
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) PopulateForm();
        }

        protected void PopulateForm()
        {
            PopulateImagesDdl(ddlComputerImage);
            PopulateSitesDdl(ddlSite);
            PopulateBuildingsDdl(ddlBuilding);
            PopulateRoomsDdl(ddlRoom);
            PopulateClusterGroupsDdl(ddlClusterGroup);
            PopulateAltServerIps(altServerIp);
        }
    }
}