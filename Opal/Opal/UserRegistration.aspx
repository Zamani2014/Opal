<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UserRegistration.aspx.cs" Inherits="UserRegistration" Title="<%$ Resources:stringsRes, pge_UserRegistration_Title%>" %>

<asp:Content runat="server" ContentPlaceHolderID="mainContent">
	<div class="roundedTop"></div>
	<div style="padding:10px;">
		<asp:CreateUserWizard 
		runat="server" 
		ID="createUserWizard" 
		DisableCreatedUser="True"
		DuplicateUserNameErrorMessage="<%$ Resources:stringsRes, ctl_CreateUserWizard_DuplicateUserName%>"
		CreateUserButtonText="<%$ Resources:stringsRes, ctl_CreateUserWizard_CreateUser%>"
		ToolTip="<%$ Resources:stringsRes, ctl_CreateUserWizard_CreateUser%>"
		DuplicateEmailErrorMessage="<%$ Resources:stringsRes, ctl_CreateUserWizard_DuplicateEmail%>"
		RequireEmail="true"
		ContinueDestinationPageUrl="~/Default.aspx"
		CreateUserButtonStyle-CssClass="button"
		>
<CreateUserButtonStyle CssClass="button"></CreateUserButtonStyle>
			<WizardSteps>
				<asp:CreateUserWizardStep runat="server" ID="createUserStep">
					<ContentTemplate>
                    <h2>
                                        <asp:Localize ID="Localize2" runat="server" 
                                            Text="فرم عمومی ثبت نام در سامانه" />
                    </h2>
                    <fieldset>
                    <legend>قوانین ثبت نام</legend>
                    <h2>قبل از تکمیل فرم حتما موارد زیر را مطالعه فرمائید :</h2>
                    <ul>
                    <li>موارد ستاره دار حتما باید تکمیل شوند .  <br /> به غیر از توضیحات که در صورت تمایل میتوانید از آن برای انتقال موارد ضروری به کارشناسان فروش استفاده کنید .</li>
                    <li>ثبت نام در سامانه رایگان میباشد اما برای استفاده از خدمات باید اعتبار خریداری کنید .</li>
                    <li>لطفا در صحت اطلاعات وارد شده در فرم دقت لازم را داشته باشید به اطلاعات اشتباه ترتیب اثر داده نخواهد شد .</li>
                    <li>اطلاعات شما نزد سامانه محفوظ خواهد بود و به هیچ عنوان بدون کسب اجازه از شما در اختیار افراد بیگانه قرار نخواهد گرفت .</li>
                    <li>در بخش اطلاعات شخصی، اطلاعات مربوط به فردی که دارای مسئولیت در قبال ثبت نام میباشد وارد گردد .</li>
                    <li>اگر شما شرکت و یا موسسه ثبت شده نیستید نیازی به تکمیل بخش مربوط به اطلاعات سازمانی نمیباشد .</li>
                    <li>کلیه فیلد های مربوط به نام کاربری و گذرواژه باید به زبان انگلیسی وارد شوند .</li>
                    <li>در صورت هر گونه سوال میتوانید با شماره های 021-33643817 و یا 09128584771 تماس حاصل فرمائید .</li>
                    <li>نمایندگان محترم حتما در بخش اطلاعات معرف نام و شماره تلفن خود را وارد نمایند .</li>
                    <li>در صورتیکه شما معرف و یا نماینده ای ندارید لزومی به تکمیل کردن بخش مربوط به معرف نمیباشد .</li>
                    <li>در صورتیکه صاحب امتیاز حساب کاربری خودتان هستید در بخش صاحب امتیاز نام و نام خانوادگی خود را تایپ کنید .</li>
                    <li>قبل از ثبت نام حتما بخش مربوط به اطلاعات حقوقی سامانه > شرایط و قوانین استفاده را مطالعه نمائید ، <br /> به هر روی ثبت نام شما به منزله مطالعه کامل و پذیرفتن شرایط و قوانین استفاده از خدمات این سامانه تلقی خواهد شد .</li>
                    </ul>
                    </fieldset>
                    <br />
                    <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False" />
                    <br />
                        <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="createUserWizard" DisplayMode="BulletList" HeaderText="لطفا به پیغام های زیر توجه فرمائید :" runat="server" />
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ValidationGroup="createUserWizard" Display="None" ErrorMessage="وارد کردن نام کاربری مناسب ضروری است ."/>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ValidationGroup="createUserWizard" Display="None" ErrorMessage="وارد کردن اسم رمز مناسب ضروری است . ."/>
						<asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" Display="None" ValidationGroup="createUserWizard" ErrorMessage="<%$Resources:stringsRes, ctl_CreateUserWizard_ComparePassword %>"/>
						<asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword" ValidationGroup="createUserWizard" Display="None" ErrorMessage="<%$ Resources:stringsRes, ctl_CreateUserWizard_ConfirmPasswordRequired%>"/>
						<asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" ValidationGroup="createUserWizard" Display="None" ErrorMessage="<%$ Resources:stringsRes, ctl_CreateUserWizard_EmailRequired%>" />
						<asp:RegularExpressionValidator ID="EmailRegexValidator" runat="server" ControlToValidate="Email" ValidationGroup="createUserWizard" Display="None" ErrorMessage="<%$Resources:stringsRes, pge_UserRegistration_EmailRegex %>" />
						<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="CompanyEmail" ValidationGroup="createUserWizard" Display="None" ValidationExpression="^(\w[-._\w]*@\w[-._\w]*\w\.\w{2,6})$" ErrorMessage="لطفا نشانی پست الکترونیکی شرکت خود را صحیح وارد نمائید ." />
                        <asp:RequiredFieldValidator ID="FirstNameRequiredFieldValidator1" runat="server" ControlToValidate="FirstName" ValidationGroup="createUserWizard" Display="None" ErrorMessage="وارد کردن نام کوچک ضروری است ."/>
                        <asp:RequiredFieldValidator ID="LastNameRequiredFieldValidator2" runat="server" ControlToValidate="LastName" ValidationGroup="createUserWizard" Display="None" ErrorMessage="وارد کردن نام خانوادگی ضروری است . ."/>
                        <asp:RequiredFieldValidator ID="NationalCodeRequiredFieldValidator3" runat="server" ControlToValidate="NationalCode" ValidationGroup="createUserWizard" Display="None" ErrorMessage="وارد کردن کد ملی معتبر ضروری است ."/>
                        <asp:RequiredFieldValidator ID="IntroduceWayRequiredFieldValidator4" runat="server" ControlToValidate="IntroduceWay" ValidationGroup="createUserWizard" Display="None" ErrorMessage="وارد کردن نحوه آشنائی با این شرکت ضروری است ."/>
                        <asp:RequiredFieldValidator ID="ConcessionaireRequiredFieldValidator5" runat="server" ControlToValidate="Concessionaire" ValidationGroup="createUserWizard" Display="None" ErrorMessage="وارد کردن نام صاحب امتیاز این حساب کاربری ضروری است ."/>
                        <asp:RequiredFieldValidator ID="GradeLevelRequiredFieldValidator6" runat="server" ControlToValidate="GradeLevel" ValidationGroup="createUserWizard" Display="None" ErrorMessage="وارد کردن میزان تحصیلات ضروری است ."/>
                        <asp:RequiredFieldValidator ID="TelRequiredFieldValidator7" runat="server" ControlToValidate="Tel" ValidationGroup="createUserWizard" Display="None" ErrorMessage="وارد کردن شماره تلفن ثابت ضروری است ."/>
                        <asp:RequiredFieldValidator ID="MobileRequiredFieldValidator8" runat="server" ControlToValidate="Mobile" ValidationGroup="createUserWizard" Display="None" ErrorMessage="وارد کردن شماره تلفن همراه ضروری است ."/>
                        <asp:RequiredFieldValidator ID="PostalCodeRequiredFieldValidator9" runat="server" ControlToValidate="PostalCode" ValidationGroup="createUserWizard" Display="None" ErrorMessage="وارد کردن کد پستی ضروری است ."/>
                        <asp:RequiredFieldValidator ID="AddressRequiredFieldValidator10" runat="server" ControlToValidate="Address" ValidationGroup="createUserWizard" Display="None" ErrorMessage="وارد کردن آدرس مناسب ضروری است ."/>
                    <br />
                    <fieldset>
                    <legend><span style="color: #FF0000">*</span> اطلاعات شخصی</legend>
						<table border="0">
							<tr>
								<td style="width: 101px"></td>
								<td>
                                    </td>
                                <td style="width: 101px">
                                </td>
								<td></td>
							</tr>
							<tr>
								<td class="fieldlabel" style="width: 101px">
                                    <asp:Label ID="FirstNameLabel" runat="server" AssociatedControlID="FirstName" 
                                        Text="نام :" />
                                </td>
								<td class="field">
                                    <asp:TextBox ID="FirstName" runat="server" Width="150px" 
                                        style="direction: rtl" MaxLength="50" />
                                </td>
                                <td class="fieldlabel" style="width: 101px">
                                    <asp:Label ID="LastNameLabel" runat="server" AssociatedControlID="LastName" 
                                        Text="نام خانوادگی :" />
                                </td>
								<td class="field">
                                    <asp:TextBox ID="LastName" Width="150px" runat="server" 
                                        style="direction: rtl" MaxLength="50"/></td>
							</tr>
							<tr>
								<td style="width: 101px"></td>
								<td>
                                    </td>
                                <td style="width: 101px">
                                </td>
								<td>
                                </td>
							</tr>
							<tr>
								<td class="fieldlabel" style="width: 101px">
                                    <asp:Label ID="Label1" runat="server" AssociatedControlID="NationalCode" 
                                        Text="کد ملی :" />
                                </td>
								<td class="field">
                                    <asp:TextBox ID="NationalCode" runat="server" 
                                        style="direction: ltr" Width="150px" MaxLength="10"/></td>
                                <td class="fieldlabel" style="width: 101px">
                                    <asp:Label ID="IntroduceLabel" runat="server" AssociatedControlID="IntroduceWay" 
                                        Text="نحوه آشنائی :" />
                                </td>
								<td class="field">
                                    <asp:TextBox ID="IntroduceWay" runat="server"  
                                        style="direction: rtl" MaxLength="50"/></td>
							</tr>
							<tr>
								<td style="width: 101px"></td>
								<td>
                                    </td>
                                <td style="width: 101px">
                                </td>
								<td>
								</td>
							</tr>
							<tr>
								<td class="fieldlabel" style="width: 101px">
                                    <asp:Label ID="Label2" runat="server" AssociatedControlID="Concessionaire" 
                                        Text="صاحب امتیاز :" />
                                </td>
								<td class="field"><asp:TextBox ID="Concessionaire" runat="server" Width="150px" 
                                        MaxLength="50"/></td>
                                <td class="fieldlabel" style="width: 101px">
                                    <asp:Label ID="GradeLevelLabel" runat="server" 
                                        AssociatedControlID="GradeLevel" 
                                        Text="میزان تحصیلات :" />
                                </td>
								<td class="field">
                                    <asp:TextBox ID="GradeLevel" runat="server" Width="150px"
                                         style="direction: rtl" MaxLength="50"/></td>
							</tr>
							<tr>
								<td style="width: 101px"></td>
								<td>
                                   </td>
                                <td style="width: 101px">
                                </td>
								<td>
								</td>
							</tr>
						</table>
                    </fieldset>
                    <br />
                    <fieldset>
                    <legend>اطلاعات سازمانی</legend>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 101px">
                                    <asp:Label ID="Label7" runat="server" Text="نام سازمان :" AssociatedControlID="CompanyName"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="CompanyName" runat="server" Width="150px" 
                                        style="direction: rtl" MaxLength="50"></asp:TextBox>
</td>
                                <td style="width: 101px">
                                    <asp:Label ID="Label8" runat="server" Text="شماره ثبت :" AssociatedControlID="RegisterNo"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="RegisterNo" runat="server" Width="150px" 
                                        style="direction: ltr" MaxLength="20"></asp:TextBox>
</td>
                            </tr>
                            <tr>
                                <td style="width: 101px">
                                    <asp:Label ID="Label9" runat="server" Text="زمینه فعالیت :" AssociatedControlID="ActivityField"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="ActivityField" runat="server" Width="150px" 
                                        style="direction: rtl" MaxLength="50"></asp:TextBox>
                                </td>
                                <td style="width: 101px">
                                    <asp:Label ID="Label10" runat="server" Text="سمت شما :" AssociatedControlID="PostInCo"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="PostInCo" runat="server" Width="150px" style="direction: rtl" 
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 101px">
                                    <asp:Label ID="Label11" runat="server" Text="نشانی اینترنتی :" AssociatedControlID="Website"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Website" runat="server" Width="150px" style="direction: ltr" 
                                        MaxLength="50"></asp:TextBox>
                                </td>
                                <td style="width: 101px">
                                    <asp:Label ID="Label12" runat="server" Text="پست الکترونیکی :" AssociatedControlID="CompanyEmail"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="CompanyEmail" runat="server" Width="150px" 
                                        style="direction: ltr" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <br />
                    <fieldset>
                    <legend><span style="color: #FF0000">*</span> اطلاعات تماس</legend>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 101px">
                                    <asp:Label ID="Label13" runat="server" Text="تلفن ثابت :" AssociatedControlID="Tel"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Tel" runat="server" style="direction: ltr" Width="150px" 
                                        MaxLength="20"></asp:TextBox>
                                </td>
                                <td style="width: 101px">
                                    <asp:Label ID="Label14" runat="server" Text="تلفن همراه :" AssociatedControlID="Mobile"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Mobile" runat="server" style="direction: ltr" Width="150px" 
                                        MaxLength="20"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 101px">
                                    <asp:Label ID="Label15" runat="server" Text="کد پستی :" AssociatedControlID="PostalCode"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="PostalCode" runat="server" Width="150px" 
                                        style="direction: ltr" MaxLength="20"></asp:TextBox>
                                </td>
                                <td style="width: 101px">
                                    <asp:Label ID="Label16" runat="server" Text="آدرس :" AssociatedControlID="Address"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Address" runat="server" style="direction: rtl" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <br />
                    <fieldset>
                    <legend>اطلاعات معرف</legend>
                    <table>
                    <tr>
                    <td style="width: 101px">
                        <asp:Label ID="Label4" runat="server" Text="نام نماینده :" AssociatedControlID="AgentName"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="AgentName" runat="server" Width="150px" MaxLength="50"></asp:TextBox></td>
                            <td style="width: 101px">
                                <asp:Label ID="Label5" runat="server" Text="تلفن نماینده :" AssociatedControlID="AgentTelNo"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="AgentTelNo" runat="server" Width="150px" 
                                        style="direction: ltr" MaxLength="20"></asp:TextBox></td>
                    </tr>
                    </table>
                    </fieldset>
                    <br />
                    <fieldset>
                    <legend><span style="color: #FF0000">*</span> اطلاعات کاربری</legend>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 101px">
                                    <asp:Label ID="UserNameLabel" runat="server" Text="نام کاربری :" AssociatedControlID="UserName" ></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="UserName" runat="server" Width="150px" style="direction: ltr" 
                                        MaxLength="50"></asp:TextBox>
                                </td>
                                <td style="width: 101px">
                                    <asp:Label ID="EmailLabel" runat="server" Text="پست الکترونیکی :" AssociatedControlID="Email"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Email" runat="server" Width="150px" style="direction: ltr" 
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 101px">
                                    <asp:Label ID="PasswordLabel" runat="server" Text="گذرواژه :" AssociatedControlID="Password"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="150px" 
                                        style="direction: ltr" MaxLength="50"></asp:TextBox>
                                </td>
                                <td style="width: 101px">
                                    <asp:Label ID="Label20" runat="server" Text="تائید گذرواژه :" AssociatedControlID="ConfirmPassword"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" 
                                        Width="150px" style="direction: ltr" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table>
                        <tr>
                        <td style="width: 101px">
                            <asp:Label ID="Label3" runat="server" Text="توضیحات :" AssociatedControlID="Comment"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="Comment" runat="server" TextMode="MultiLine" MaxLength="100"></asp:TextBox></td>
                        </tr>
                        </table>
                    </fieldset>
                    <br />
                    <fieldset>
                    <table>
                            <tr>
                                <td class="fieldlabel" style="width: 101px"><span style="color: #FF0000">*</span>
                                    <asp:Label ID="Label6" runat="server" AssociatedControlID="txtAntiBotImage" 
                                        Text="تصویر ضد بات :" />
                                    </td>
                                <td class="field">&nbsp;
                                <asp:Image ID="imgAntiBotImage" runat="server" ImageUrl="~/antibotimage.ashx" GenerateEmptyAlternateText="true" /><br />
                                <asp:TextBox ID="txtAntiBotImage" MaxLength="4" runat="server" ValidationGroup="createUserWizard" style="direction: ltr; width:100px"/></td>
                                <td style="width: 101px" >
                                    
                                    </td>
                                <td class="field">
                            <asp:RequiredFieldValidator runat="server" ID="valAntiBotImageRequired" ControlToValidate="txtAntiBotImage" Display="None" ValidationGroup="createUserWizard" ErrorMessage="<%$ Resources:stringsRes, ctl_Guestbook_ErrorMessageAntibot %>" SetFocusOnError="true"></asp:RequiredFieldValidator><br />
                            <asp:CustomValidator runat="server" ID="valAntiBotImage" ValidationGroup="createUserWizard" OnServerValidate="valAntiBotImage_ServerValidate" Text="<%$ Resources:stringsRes, ctl_Guestbook_ErrorMessageAntibotInvalid %>" Display="None"></asp:CustomValidator>
                                    </td>
                            </tr>
                    </table>
                    <table>
                    <tr>
                    <td>در صورتیکه پیش از این از سامانه های پیامک موجود در بازار فناوری اطلاعات استفاده کرده اید نام شرکت و سامانه مربوطه را وارد نمائید ؟<br />
                        <asp:TextBox ID="OldSMS" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    </table>
                        <asp:CheckBox ID="Subscriber" runat="server" Text="مایل به دریافت پیامک و ایمیل های اطلاع رسانی از سوی شرکت آروید فاوا هستم ." />
                    </fieldset>
					</ContentTemplate>
				</asp:CreateUserWizardStep>
				<asp:CompleteWizardStep runat="server">
					<ContentTemplate>
						<h2><asp:Localize ID="Localize1" runat="server" Text="<%$Resources:stringsRes, pge_UserRegistration_WizardTitle %>" /></h2>
						<p><strong><asp:Localize runat="server" Text="<%$Resources:stringsRes, pge_UserRegistration_ConfirmationTitle %>" /></strong></p>
						<p><asp:Localize runat="server" Text="<%$Resources:stringsRes, pge_UserRegistration_ConfirmationText %>" /></p>
						<p><asp:Button ID="ContinueButton" runat="server" CausesValidation="False" CommandName="Continue" Text="<%$Resources:stringsRes, pge_UserRegistration_ContinueButton %>" ValidationGroup="createUserWizard" /></p>
					</ContentTemplate>
				</asp:CompleteWizardStep>
			</WizardSteps>
		</asp:CreateUserWizard>
	</div>
	<div class="roundedBottom"></div>
</asp:Content>
