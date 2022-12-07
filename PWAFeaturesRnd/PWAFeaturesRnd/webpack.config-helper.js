"use strict";

const Path = require("path");
const Webpack = require("webpack");
const ExtractTextPlugin = require("extract-text-webpack-plugin");
const ExtractSASS = new ExtractTextPlugin("./compiled-style.css");
const CopyWebpackPlugin = require("copy-webpack-plugin");
const BrowserSyncPlugin = require("browser-sync-webpack-plugin");
const webpack = require("webpack");

module.exports = (options) => {
    const dest = Path.join(__dirname, "wwwroot");

    let webpackConfig = {
        devtool: options.devtool,
        entry: {
            main: "./src/app.js",
            demo: "./src/scripts-init/demo.js",
            ladda: "./src/scripts-init/ladda-loading.js",
            blockui: "./src/scripts-init/blockui.js",
            circle_progress: "./src/scripts-init/circle-progress.js",
            count_up: "./src/scripts-init/count-up.js",
            toastr: "./src/scripts-init/toastr.js",
            sweet_alerts: "./src/scripts-init/sweet-alerts.js",
            scrollbar: "./src/scripts-init/scrollbar.js",
            sticky_elements: "./src/scripts-init/sticky-elements.js",
            carousel_slider: "./src/scripts-init/carousel-slider.js",
            fullcalendar: "./src/scripts-init/calendar.js",
            treeview: "./src/scripts-init/treeview.js",
            maps: "./src/scripts-init/maps.js",
            rating: "./src/scripts-init/rating.js",
            image_crop: "./src/scripts-init/image-crop.js",
            guided_tours: "./src/scripts-init/guided-tours.js",
            tables: "./src/scripts-init/tables.js",

            form_validation: "./src/scripts-init/form-components/form-validation.js",
            form_wizard: "./src/scripts-init/form-components/form-wizard.js",
            clipboard: "./src/scripts-init/form-components/clipboard.js",
            datepicker: "./src/scripts-init/form-components/datepicker.js",
            input_mask: "./src/scripts-init/form-components/input-mask.js",
            input_select: "./src/scripts-init/form-components/input-select.js",
            range_slider: "./src/scripts-init/form-components/range-slider.js",
            textarea_autosize:
                "./src/scripts-init/form-components/textarea-autosize.js",
            toggle_switch: "./src/scripts-init/form-components/toggle-switch.js",

            chart_js: "./src/scripts-init/charts/chartjs.js",
            apex_charts: "./src/scripts-init/charts/apex-charts.js",
            sparklines: "./src/scripts-init/charts/charts-sparklines.js",
            matchheight: "./wwwroot/scripts/lib/jquery.matchHeight.js",
            common: "./wwwroot/scripts/common/common.js",

            jquery: "./wwwroot/scripts/lib/validation/jquery-3.5.1.js",
            jquery_validator: "./wwwroot/scripts/lib/validation/jquery.validate.min.js",
            jquery_validator_unobtrusive: "./wwwroot/scripts/lib/jquery-validate-unobtrusive/jquery.validate.unobtrusive.min.js",
            jquery_validator_ajax: "./wwwroot/scripts/lib/jquery-ajax-unobtrusive/dist/jquery.unobtrusive-ajax.min.js",

            dashboard: "./wwwroot/scripts/dashboard/dashboard.js",
            login: "./wwwroot/scripts/login/login.js",
            viewPPMList: "./wwwroot/scripts/master/viewPPMList.js",
            finanaceReport: "./wwwroot/scripts/master/finanaceReport.js",
            purchasingList: "./wwwroot/scripts/master/purchasingList.js",
            inspectionList: "./wwwroot/scripts/master/inspectionList.js",
            inspectionVVRFindings: "./wwwroot/scripts/master/inspection/vvr/vvrFindings.js",
            orderLinesList: "./wwwroot/scripts/master/orderlines.js",
            crewDetails: "./wwwroot/scripts/master/crewdetails.js",
            crewList: "./wwwroot/scripts/master/crewlist.js",
            positionlist: "./wwwroot/scripts/master/positionlist.js",
            changeOrderStatus: "./wwwroot/scripts/master/changeOrderStatus.js",
            vesselPositionList: "./wwwroot/scripts/master/vesselPositionList.js",
            orderDetail: "./wwwroot/scripts/master/orderDetail.js",
            defectsList: "./wwwroot/scripts/master/defectsList.js",
            defectDetails: "./wwwroot/scripts/master/defectDetails.js",
            inspectionFindingsList: "./wwwroot/scripts/master/inspectionFindingsList.js",
            inspectionActionsList: "./wwwroot/scripts/master/inspectionActionsList.js",
            vesselDetail: "./wwwroot/scripts/master/vesselDetail.js",
            hazOccList: "./wwwroot/scripts/master/hazOccList.js",
            hazOccDetails: "./wwwroot/scripts/master/hazOccDetails.js",
            viewQuote: "./wwwroot/scripts/master/viewQuote.js",
            portCalllocation: "./wwwroot/scripts/master/portCalllocation.js",
            seaPassage: "./wwwroot/scripts/master/seaPassage.js",
            pmsDetails: "./wwwroot/scripts/master/pmsDetails.js",
            financeTransactionReport: "./wwwroot/scripts/master/financeTransactionReport.js",
            pmsList: "./wwwroot/scripts/master/pmsList.js",
            maintenanceHistoryList: "./wwwroot/scripts/master/maintenanceHistoryList.js",
            portCallLocationEvent: "./wwwroot/scripts/master/portCallLocationEvent.js",
            seaPassageEvent: "./wwwroot/scripts/master/seaPassageEvent.js",
            medicalSignOffList: "./wwwroot/scripts/master/medicalSignOffList.js",
            generalLedgerTransaction: "./wwwroot/scripts/master/generalLedgerTransaction.js",
            maintenanceHistoryDetails: "./wwwroot/scripts/master/maintenanceHistoryDetails.js",
            vesselDetailsMobile: "./wwwroot/scripts/master/vesselDetailsMobile.js",
            notification: "./wwwroot/scripts/master/notification.js",
            dashboardMapFullScreen: "./wwwroot/scripts/master/dashboardMapFullScreen.js",
            moduleAccessDenied: "./wwwroot/scripts/master/moduleAccessDenied.js",
            signalRTest: "./wwwroot/scripts/master/signalrtest.js",
            notificationMobileChatDetail: "./wwwroot/scripts/master/notificationMobileChatDetail.js",
            notificationMobileInfo: "./wwwroot/scripts/master/notificationMobileInfo.js",
            notificationMobileDiscussion: "./wwwroot/scripts/master/notificationMobileDiscussion.js",
            certificateDetails: "./wwwroot/scripts/master/certificateDetails.js",
            jsaList: "./wwwroot/scripts/master/jsaList.js",
            jsaDetails: "./wwwroot/scripts/master/jsaDetails.js",
            approvalList: "./wwwroot/scripts/master/approvalList.js",
            sentinelVesselDetail: "./wwwroot/scripts/master/sentinel/vesselDetail.js",
            sentinelOfficeList: "./wwwroot/scripts/master/sentinel/officeList.js",
            sentinelVesselList: "./wwwroot/scripts/master/sentinel/vesselList.js",
            sentinelFleetVesselList: "./wwwroot/scripts/master/sentinel/fleetvesselList.js",
            vvrList: "./wwwroot/scripts/master/inspection/vvr/vvrList.js",
        },
        output:
        {
            filename: './assets/scripts/[name].bundle.js',
            path: Path.join(__dirname, 'wwwroot')
        },
        plugins: [
            new Webpack.ProvidePlugin({
                $: "jquery",
                jQuery: "jquery",
                "window.jQuery": "jquery",
                Tether: "tether",
                "window.Tether": "tether",
                Popper: ["popper.js", "default"],
            }),
            new CopyWebpackPlugin([
                { from: "./src/assets/images", to: "./assets/images" },
            ]),
            new CopyWebpackPlugin([
                { from: "./src/assets/fonts", to: "./assets/fonts" },
            ]),
            new Webpack.DefinePlugin({
                "process.env": {
                    NODE_ENV: JSON.stringify(
                        options.isProduction ? "production" : "development"
                    ),
                },
            }),
        ],
        module: {
            rules: [
                {
                    test: /\.js$/,
                    exclude: /node_modules/,
                    loader: "babel-loader",
                },
                {
                    test: /\.(woff(2)?|ttf|eot|svg)(\?v=\d+\.\d+\.\d+)?$/,
                    use: [
                        {
                            loader: "file-loader",
                            options: {
                                name: "[name].[ext]",
                                outputPath: "./assets/fonts",
                            },
                        },
                    ],
                },
                {
                    test: /\.(gif|jpg|png)$/,
                    loader: "file-loader",
                    options: {
                        name: "[name].[ext]",
                        outputPath: "./assets/images",
                    },
                },
            ],
        }
    };

    if (options.isProduction) {
        webpackConfig.entry = {
            chatmain: [
                "./src/app.js",
                /*"./src/scripts-init/demo.js",*/
                "./src/scripts-init/ladda-loading.js",
                "./src/scripts-init/blockui.js",
                "./src/scripts-init/circle-progress.js",
                "./src/scripts-init/count-up.js",
                "./src/scripts-init/toastr.js",
                "./src/scripts-init/sweet-alerts.js",
                /*"./src/scripts-init/scrollbar.js",*/
                "./src/scripts-init/sticky-elements.js",
                /*"./src/scripts-init/carousel-slider.js",*/
                /*"./src/scripts-init/calendar.js",*/
                /*"./src/scripts-init/treeview.js",*/
                //"./src/scripts-init/maps.js",
                //"./src/scripts-init/rating.js",
                //"./src/scripts-init/image-crop.js",
                //"./src/scripts-init/guided-tours.js",

                //"./src/scripts-init/form-components/form-validation.js",
                //"./src/scripts-init/form-components/form-wizard.js",
                //"./src/scripts-init/form-components/clipboard.js",
                //"./src/scripts-init/form-components/datepicker.js",
                "./src/scripts-init/form-components/input-mask.js",
                "./src/scripts-init/form-components/input-select.js",
                /*"./src/scripts-init/form-components/range-slider.js",*/
                "./src/scripts-init/form-components/textarea-autosize.js",
                "./src/scripts-init/form-components/toggle-switch.js",

                /*"./src/scripts-init/charts/chartjs.js",*/
                //"./src/scripts-init/charts/apex-charts.js",
                //"./src/scripts-init/charts/charts-sparklines.js",
                "./wwwroot/scripts/lib/validation/jquery.validate.min.js",
                "./wwwroot/scripts/lib/jquery-validate-unobtrusive/jquery.validate.unobtrusive.min.js",
                "./wwwroot/scripts/lib/jquery-ajax-unobtrusive/dist/jquery.unobtrusive-ajax.min.js",
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/common/jquery.signalR.min.js"
            ],
            main: [
                "./src/app.js",
                "./src/scripts-init/demo.js",
                "./src/scripts-init/ladda-loading.js",
                "./src/scripts-init/blockui.js",
                "./src/scripts-init/circle-progress.js",
                "./src/scripts-init/count-up.js",
                "./src/scripts-init/toastr.js",
                "./src/scripts-init/sweet-alerts.js",
                "./src/scripts-init/scrollbar.js",
                "./src/scripts-init/sticky-elements.js",
                "./src/scripts-init/carousel-slider.js",
                "./src/scripts-init/calendar.js",
                "./src/scripts-init/treeview.js",
                //"./src/scripts-init/maps.js",
                "./src/scripts-init/rating.js",
                "./src/scripts-init/image-crop.js",
                "./src/scripts-init/guided-tours.js",

                "./src/scripts-init/form-components/form-validation.js",
                "./src/scripts-init/form-components/form-wizard.js",
                "./src/scripts-init/form-components/clipboard.js",
                "./src/scripts-init/form-components/datepicker.js",
                "./src/scripts-init/form-components/input-mask.js",
                "./src/scripts-init/form-components/input-select.js",
                "./src/scripts-init/form-components/range-slider.js",
                "./src/scripts-init/form-components/textarea-autosize.js",
                "./src/scripts-init/form-components/toggle-switch.js",

                "./src/scripts-init/charts/chartjs.js",
                "./src/scripts-init/charts/apex-charts.js",
                "./src/scripts-init/charts/charts-sparklines.js",
                "./wwwroot/scripts/lib/validation/jquery.validate.min.js",
                "./wwwroot/scripts/lib/jquery-validate-unobtrusive/jquery.validate.unobtrusive.min.js",
                "./wwwroot/scripts/lib/jquery-ajax-unobtrusive/dist/jquery.unobtrusive-ajax.min.js",
                "./wwwroot/scripts/common/common.js",
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/common/jquery.signalR.min.js"//,
                // "./wwwroot/scripts/common/OfflineModal.js"
            ],
            login: [
                "./wwwroot/scripts/login/login.js"
            ],
            dashboard: [
                "./src/scripts-init/maps.js",
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/pwatour/bootstrap-tourist.js",
                "./wwwroot/scripts/dashboard/dashboard.js"
            ],
            ppmlist: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/master/viewPPMList.js"
            ],
            financeReport: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/master/finanaceReport.js"
            ],
            purchasingList: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/daterangepicker/build/vendor/jquery.hammer.js",
                "./wwwroot/daterangepicker/js/caleran.js",
                "./wwwroot/scripts/master/purchasingList.js"
            ],
            generalLedger: [
                "./src/scripts-init/tables.js",
                "./wwwroot/daterangepicker/build/vendor/jquery.hammer.js",
                "./wwwroot/daterangepicker/js/caleran.js",
                "./wwwroot/scripts/master/generalLedger.js"
            ],
            inspectionList: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwroot/scripts/lib/datatables.buttons.min.js",
                "./wwroot/scripts/lib/buttons.print.min.js",
                "./wwwroot/scripts/master/inspectionList.js"
            ],
            inspectionVVRFindings: [
                "./wwwroot/scripts/master/inspection/vvr/vvrFindings.js"
            ],
            orderLinesList: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/orderlines.js"
            ],
            crewDetails: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/lib/dataTables.checkboxes.min.js",
                "./wwwroot/scripts/master/crewdetails.js"
            ],
            crewList: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/daterangepicker/build/vendor/jquery.hammer.js",
                "./wwwroot/daterangepicker/js/caleran.js",
                "./wwwroot/scripts/lib/jquery.multiselect.js",
                "./wwwroot/scripts/master/crewlist.js"
            ],
            requisitionDetails: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/requisitionDetails.js"
            ],
            certificate: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/daterangepicker/build/vendor/jquery.hammer.js",
                "./wwwroot/daterangepicker/js/caleran.js",
                "./wwwroot/scripts/lib/dataTables.checkboxes.min.js",
                "./wwwroot/scripts/master/certificate.js"
            ],
            positionlist: [
                "./src/scripts-init/maps.js",
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/master/positionlist.js"
            ],
            changeOrderStatus: [
                "./src/scripts-init/form-components/datepicker.js",
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/master/changeOrderStatus.js",
            ],
            vesselPositionList: [
                "./src/scripts-init/maps.js",
                "./src/scripts-init/tables.js",
                "./wwwroot/daterangepicker/build/vendor/jquery.hammer.js",
                "./wwwroot/daterangepicker/js/caleran.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/vesselPositionList.js"
            ],
            orderDetail: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/lib/dataTables.checkboxes.min.js",
                "./wwwroot/scripts/master/orderDetail.js",
                "./wwwroot/scripts/master/lookup/insuranceClaimAuxlookup.js",
                "./wwwroot/scripts/master/lookup/seasonalAuxlookup.js",
                "./wwwroot/scripts/master/lookup/nationalityAuxLookup.js",
                "./wwwroot/scripts/master/lookup/crewRankAuxLookup.js",
                "./wwwroot/scripts/master/lookup/vesselAuxLookup.js",
                "./wwwroot/scripts/master/lookup/general1AuxLookup.js",
                "./wwwroot/scripts/master/lookup/general3AuxLookup.js",
            ],
            defectsList: [
                "./src/scripts-init/tables.js",
                //"./wwwroot/scripts/lib/dataTables.colReorder.min.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/lib/dataTables.checkboxes.min.js",
                "./wwwroot/daterangepicker/build/vendor/jquery.hammer.js",
                "./wwwroot/daterangepicker/js/caleran.js",
                "./wwwroot/scripts/master/defectsList.js",
                "./wwwroot/scripts/lib/jquery.multiselect.js"
            ],
            defectDetails: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/defectDetails.js",
            ],
            inspectionFindingsList: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/inspectionFindingsList.js"
            ],
            inspectionActionsList: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/inspectionActionsList.js"
            ],
            vesselDetail: [
                "./src/scripts-init/maps.js",
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./src/scripts-init/form-components/datepicker.js",
                "./wwwroot/scripts/master/vesselDetail.js",
                "./wwwroot/scripts/master/voyageReportingModal.js"
            ],
            inspectionList: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                /*"./src/scripts-init/form-components/datepicker.js",*/
                "./wwwroot/daterangepicker/build/vendor/jquery.hammer.js",
                "./wwwroot/daterangepicker/js/caleran.js",
                "./wwwroot/scripts/master/inspectionList.js",
                "./wwwroot/scripts/master/lookup/companyLookUp.js"
            ],
            vvrList: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./src/scripts-init/form-components/datepicker.js",
                "./wwwroot/daterangepicker/build/vendor/jquery.hammer.js",
                "./wwwroot/daterangepicker/js/caleran.js",
                "./wwwroot/scripts/master/inspection/vvr/vvrList.js"

            ],
            hazOccList: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/daterangepicker/build/vendor/jquery.hammer.js",
                "./wwwroot/daterangepicker/js/caleran.js",
                "./wwwroot/scripts/master/hazOccList.js"
            ],
            hazOccDetails: [
                "./src/scripts-init/form-components/datepicker.js",
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/hazOccDetails.js"
            ],
            viewQuote: [
                "./src/scripts-init/form-components/datepicker.js",
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/viewQuote.js",
            ],
            portCalllocation: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/master/portCalllocation.js"
            ],
            seaPassage: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/master/seaPassage.js"
            ],
            pmsDetails: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/pmsDetails.js",
            ],
            financeTransactionReport: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/master/financeTransactionReport.js"
            ],
            pmsList: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/daterangepicker/build/vendor/jquery.hammer.js",
                "./wwwroot/daterangepicker/js/caleran.js",
                "./wwwroot/scripts/master/pmsList.js"
            ],
            maintenanceHistoryList: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/daterangepicker/build/vendor/jquery.hammer.js",
                "./wwwroot/daterangepicker/js/caleran.js",
                "./wwwroot/scripts/master/maintenanceHistoryList.js"
            ],
            portCallLocationEvent: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/master/voyageReportingModal.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/portCallLocationEvent.js",
            ],
            seaPassageEvent: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/seaPassageEvent.js",
            ],
            medicalSignOffList: [
                "./wwwroot/daterangepicker/build/vendor/jquery.hammer.js",
                "./wwwroot/daterangepicker/js/caleran.js",
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/master/medicalSignOffList.js",
            ],
            generalLedgerTransaction: [
                "./src/scripts-init/tables.js",
                "./wwwroot/daterangepicker/build/vendor/jquery.hammer.js",
                "./wwwroot/daterangepicker/js/caleran.js",
                "./wwwroot/scripts/master/generalLedgerTransaction.js",
            ],
            maintenanceHistoryDetails: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/maintenanceHistoryDetails.js"
            ],
            vesselDetailsMobile: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/vesselDetailsMobile.js"
            ],
            notification: [
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/notification.js"
            ],
            dashboardMapFullScreen: [
                "./wwwroot/scripts/master/dashboardMapFullScreen.js"
            ],
            moduleAccessDenied: [
                "./wwwroot/scripts/master/moduleAccessDenied.js"
            ],
            signalRTest: [
                "./wwwroot/scripts/master/signalrtest.js"
            ],
            notificationMobileChatDetail: [
                "./wwwroot/scripts/master/notificationMobileChatDetail.js"
            ],
            notificationMobileInfo: [
                "./wwwroot/scripts/master/notificationMobileInfo.js"
            ],
            notificationMobileDiscussion: [
                "./wwwroot/scripts/master/notificationMobileDiscussion.js"
            ],
            notificationChat: [
                "./wwwroot/scripts/master/notificationchat.js"
            ],
            certificateDetails: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/certificateDetails.js"
            ],
            jsaList: [
                "./src/scripts-init/tables.js",
                "./src/scripts-init/form-components/datepicker.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/jsaList.js"
            ],
            jsaDetails: [
                "./src/scripts-init/form-components/datepicker.js",
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/jsaDetails.js"
            ],
            approvalList: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/approvalList.js",
            ],
            sentinelVesselDetail: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/sentinel/vesselDetail.js"
            ],
            sentinelOfficeList: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/sentinel/officeList.js"
            ],
            sentinelVesselList: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/sentinel/vesselList.js"
            ],
            sentinelFleetVesselList: [
                "./src/scripts-init/tables.js",
                "./wwwroot/scripts/lib/jquery.matchHeight.js",
                "./wwwroot/scripts/master/sentinel/fleetVesselList.js"
            ],
        };

        webpackConfig.plugins.push(
            ExtractSASS
        );

        webpackConfig.module.rules.push(
            {
                test: /\.scss$/i,
                use: ExtractSASS.extract(["css-loader", "sass-loader"]),
            },
            {
                test: /\.css$/i,
                use: ExtractSASS.extract(["css-loader"]),
            }
        );
    } else {
        webpackConfig.plugins.push(new Webpack.HotModuleReplacementPlugin());

        webpackConfig.module.rules.push(
            {
                test: /\.scss$/i,
                use: [
                    "style-loader?sourceMap",
                    "css-loader?sourceMap",
                    "sass-loader?sourceMap",
                ],
            },
            {
                test: /\.css$/i,
                use: ["style-loader", "css-loader"],
            },
            {
                test: /\.js$/,
                use: "eslint-loader",
                exclude: /node_modules/,
            }
        );

        webpackConfig.devServer = {
            port: options.port,
            contentBase: dest,
            historyApiFallback: true,
            compress: options.isProduction,
            inline: !options.isProduction,
            hot: !options.isProduction,
            stats: {
                chunks: true,
            },
        };

        webpackConfig.plugins.push(
            new BrowserSyncPlugin(
                {
                    host: "localhost",
                    port: 3001,
                    files: ["public/**/*.*"],
                    browser: "google chrome",
                    reloadDelay: 1000,
                },
                {
                    reload: false,
                }
            )
        );
    }

    return webpackConfig;
};
