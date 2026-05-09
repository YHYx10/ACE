<template>
  <div class="hud">
    <hud-left :zoomDesign="zoomDesign" />

    <div class="hud-right">
      <HudRight />

      <Speedometer v-if="inVeh" />

      <Chips :style="'zoom:' + zoomDesign + ''" />
    </div>

    <level-up v-if="levelUp.show" :style="'zoom:' + zoomDesign + ''" />
    <promt v-if="promptData.show" :style="'zoom:' + zoomDesign + ''" />

    <ArrestTimer v-if="artimer" :timer="artimer" />
    <!-- <State /> -->
    <QuestMessage />
    <!-- <Speaker /> -->
    <capture /><!--  -->
    <Speaker />
    <!-- <Help /> -->
    <TimeToCapture />
    <FishingMiniGame v-if="miniGame" />
    <!-- <FishingAction v-if="showAction" /> -->
    <kill-stat v-if="isKillstat" />
    <KilLog />
    <full-kill-stat />
    <work-timer
      v-if="workTimer"
      :style="{
        left: `calc(${
          this.minimap.leftX * 100 + 'vw'
        })`,
      }"
    />
    <!-- <notification v-if="notificationShow" /> -->
    <drift-score v-if="driftScore.show" />
    <!-- <green-zone
      v-if="isGreenZone"
      :bottom-offset="(1 - this.minimap.bottomY) * 100"
      :right-offset="this.minimap.rightX * 100 + 1"
    /> -->
    <!-- <level-up v-if="levelUp.show" /> -->
    <tip v-if="tip.show" :style="'zoom:' + zoomDesign + ''" />
    <phase-timer v-if="phaseTimer.show" />
    <!-- <win-notification v-if="winNotification.show" /> -->
    <RemoteController v-if="remoteController.show" />
    <DonationIncrease />
    <WarZone v-if="warZone.show" />
    <HoleInformation v-if="holeInformation.show && !inVeh" />
    <template v-if="showWarForEnterprice">
      <WarForEnterpriceCurrent v-if="currentTime && currentTime > 0" :zoom="zoomDesign" :inVeh="inVeh"/>
      <WarForEnterpriceList
        v-if="
          captureList.filter((element) => element.time >= element.timePassed)
            .length > 0
        "
      />
    </template>
    <TransferReport v-if="transferReport.show" />
    <RobberyShop />
    <BigInfo :style="'zoom:' + zoomDesign + ''" />
  </div>
</template>

<script>
import { mapState } from "vuex";
import Speaker from "./Speaker";
import Speedometer from "../Speedometer";
/* import Help from "./Help"; */
import FishingMiniGame from "../Fishing/components/MiniGame";
/* import FishingAction from "../Fishing/components/FishingAction"; */
import KillStat from "./KillStat";
import FullKillStat from "./FullKillStat";
import WorkTimer from "./WorkTimer";
import Capture from "./Capture";
import TimeToCapture from "./TimeToCapture";
import DriftScore from "./DriftScore";
import LevelUp from "./LevelUp";
import Tip from "./Tip";
import ArrestTimer from "./ArrestTimer";
import PhaseTimer from "./PhaseTimer";
import RemoteController from "./RemoteController";
import DonationIncrease from "./DonationIncrease";
import WarZone from "./WarZone";
import WarForEnterpriceCurrent from "./WarForEnterprice/WarForEnterpriceCurrent";
import WarForEnterpriceList from "./WarForEnterprice/WarForEnterpriceList";
import HoleInformation from "./HoleInformation";
import TransferReport from "./TransferReport";
import QuestMessage from "./QuestMessage.vue";
import BigInfo from "./BigInfo.vue";
import RobberyShop from "../RobberyShop/Index.vue";
import HudRight from "./HudRight.vue";
import Promt from "./Promt.vue";
import HudLeft from "./HudLeft.vue";
import Chips from "./Chips.vue";
import KilLog from './KilLog.vue';

export default {
  data() {
    return {
      zoomDesign: 1,
    };
  },
  components: {
    Capture,
    Speaker,
    Speedometer,
    /* Help, */
    FishingMiniGame,
    /* FishingAction, */
    KillStat,
    FullKillStat,
    WorkTimer,
    TimeToCapture,
    DriftScore,
    Tip,
    LevelUp,
    ArrestTimer,
    PhaseTimer,
    RemoteController,
    DonationIncrease,
    WarZone,
    WarForEnterpriceCurrent,
    WarForEnterpriceList,
    HoleInformation,
    TransferReport,
    QuestMessage,
    BigInfo,
    RobberyShop,
    HudRight,
    Promt,
    HudLeft,
    Chips,
    KilLog
},
  computed: {
    ...mapState("fishing", ["miniGame", "showAction"]),
    ...mapState("hud", [
      "mic",
      "levelUp",
      "promptData",
      "isKillstat",
      "workTimer",
      "notificationShow",
      "driftScore",
      "isGreenZone",
      "levelUp",
      "tip",
      "minimap",
      "artimer",
      "phaseTimer",
      "winNotification",
      "remoteController",
      "donationIncrease",
      "warZone",
      "holeInformation",
      "transferReport",
      "bonusDonateMoney"
    ]),
    ...mapState("speedometer", ["inVeh"]),
    ...mapState("hud/warForEnterprice", [
      "showWarForEnterprice",
      "currentTime",
      "captureList",
    ])
  },
  mounted() {
    this.handleResize();
    window.addEventListener("resize", this.handleResize);
  },
  beforeDestroy() {
    window.removeEventListener("resize", this.handleResize);
  },
  methods: {
    handleResize() {
      let zoomCountOne = document.body.clientWidth / 1920; //1920;
      let zoomCountTwo = document.body.clientHeight / 1080; //1080;

      if (zoomCountOne < zoomCountTwo) this.zoomDesign = zoomCountOne;
      else this.zoomDesign = zoomCountTwo;
    },
  },
};
</script>

<style lang="scss">
.hud {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  pointer-events: none;

  &-right {
    position: absolute;
    top: 0.95rem;
    right: 2.125rem;
    height: 100%;
    display: flex;
    flex-direction: column;
    align-items: flex-end;
    justify-content: flex-start;

    & > div:first-child {
      display: flex;
      flex-direction: column;
      align-items: flex-end;
      position: relative;
    }
  }
}
</style>
