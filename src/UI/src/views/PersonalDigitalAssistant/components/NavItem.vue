<template>
  <div
    :class="[
      { active: item.key === currentPage },
      { army: type === 2 },
      { fbi: type === 1 },
      'header-item',
    ]"
    @click="setCurrentPage(item.key)"
  >
    <span class="nav-icon">
      <img :src="navIconSrc" alt="" />
    </span>
    <span>{{ item.label || loc(item.text) }}</span>
    <span class="count" v-if="item.key === 'HelpPage' && helpList.length > 0">{{
      helpList.length
    }}</span>
  </div>
</template>

<script>
import { mapGetters, mapMutations, mapState } from "vuex";
export default {
  name: "NavItem",

  props: {
    item: Object,
  },

  computed: {
    ...mapState("personalDigitalAssistant", [
      "currentPage",
      "helpList",
      "type",
    ]),
    ...mapGetters("localization", ["loc"]),
    navIconClass: function () {
      const icons = {
        HelpPage: "dispatch",
        FindHumanPage: "citizen",
        FindCarPage: "vehicle",
        WantedListPage: "wanted",
        DataBasePage: "database",
        IssueFinePage: "fine",
        AddWantedPage: "wanted",
        PlayersNearbyPage: "nearby",
      };
      return icons[this.item.key] || "database";
    },
    navIconSrc: function () {
      const icons = {
        HelpPage: "calls.svg",
        FindHumanPage: "search.svg",
        FindCarPage: "search.svg",
        WantedListPage: "wanted-list.svg",
        DataBasePage: "wanted-add.svg",
        IssueFinePage: "fine.svg",
        AddWantedPage: "wanted-add.svg",
        PlayersNearbyPage: "players-nearby.svg",
      };
      return `/img/mdt/${icons[this.item.key] || "search.svg"}`;
    },
  },

  methods: {
    ...mapMutations("personalDigitalAssistant", ["setCurrentPage"]),
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.header-item {
  display: flex;
  align-items: center;
  min-height: conv(54);
  padding: 0 conv(18);
  position: relative;
  overflow: hidden;
  transition: 0.18s ease;
  border-radius: conv(10);
  border: 1px solid rgba(255, 255, 255, 0.065);
  background: rgba(255, 255, 255, 0.035);
  font-weight: 800;
  font-size: conv(15);
  line-height: conv(18);
  letter-spacing: 0.055em;
  text-transform: uppercase;
  color: rgba(255, 255, 255, 0.72);
  cursor: pointer;

  span {
    z-index: 3;
  }

  &:hover {
    color: #fff;
    border-color: rgba(31, 139, 255, 0.34);
    background: rgba(31, 139, 255, 0.1);
    transform: translateX(conv(3));
  }

  &::before {
    content: "";
    position: absolute;
    left: 0;
    top: 18%;
    width: conv(3);
    height: 64%;
    border-radius: 999px;
    background: rgba(31, 139, 255, 0.85);
    box-shadow: 0 0 0.75rem rgba(31, 139, 255, 0.5);
    opacity: 0;
    transition: 0.18s ease;
  }

  &::after {
    z-index: 2;
    content: "";
    position: absolute;
    inset: 0;
    opacity: 0;
    transition: 0.18s ease;
    background: linear-gradient(90deg, rgba(31, 139, 255, 0.2), rgba(31, 139, 255, 0.035));
  }

  &.active {
    color: #fff;
    border-color: rgba(31, 139, 255, 0.42);
    background: rgba(31, 139, 255, 0.12);

    &::before,
    &::after {
      opacity: 1;
    }
  }

  &:last-child {
    pointer-events: none;
    cursor: auto;
    opacity: 0.35;
  }

  .count {
    position: absolute;
    top: 50%;
    right: conv(14);
    transform: translateY(-50%);
    min-width: conv(28);
    height: conv(26);
    padding: 0 conv(8);
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: 999px;
    font-weight: 900;
    font-size: conv(13);
    line-height: conv(16);
    text-align: center;
    text-transform: uppercase;
    color: #ffffff;
    background: #1f8bff;
    box-shadow: 0 0 0.9rem rgba(31, 139, 255, 0.35);
  }
}

/* Premium MDT navigation pass */
.header-item {
  min-height: conv(48);
  padding: 0 conv(13);
  gap: conv(10);
  border-radius: conv(8);
  background:
    linear-gradient(90deg, rgba(31, 139, 255, 0.055), rgba(255, 255, 255, 0.025)),
    rgba(4, 13, 26, 0.48);
  border-color: rgba(103, 188, 255, 0.11);
  box-shadow: inset 0 0 conv(18) rgba(0, 0, 0, 0.14);

  .nav-icon {
    z-index: 3;
    width: conv(25);
    height: conv(25);
    min-width: conv(25);
    display: flex;
    align-items: center;
    justify-content: center;
    border-radius: conv(7);
    background: rgba(255, 255, 255, 0.055);
    border: 1px solid rgba(255, 255, 255, 0.07);
    color: rgba(116, 202, 255, 0.9);
    box-shadow: inset 0 0 conv(12) rgba(31, 139, 255, 0.06);

    &::before {
      display: none;
    }

    img {
      display: block;
      width: conv(17);
      height: conv(17);
      object-fit: contain;
      filter: drop-shadow(0 0 conv(10) rgba(66, 181, 255, 0.5));
    }
  }

  > span:not(.count):not(.nav-icon) {
    flex: 1;
  }

  &:hover {
    transform: translateX(conv(5));
    border-color: rgba(83, 180, 255, 0.42);
    box-shadow:
      inset 0 0 conv(18) rgba(31, 139, 255, 0.07),
      0 0 conv(18) rgba(31, 139, 255, 0.13);
  }

  &::before {
    width: conv(4);
    box-shadow: 0 0 conv(15) rgba(53, 172, 255, 0.78);
  }

  &.active {
    border-color: rgba(86, 184, 255, 0.58);
    box-shadow:
      inset conv(4) 0 0 rgba(65, 176, 255, 0.92),
      inset 0 0 conv(28) rgba(31, 139, 255, 0.12),
      0 0 conv(22) rgba(31, 139, 255, 0.18);

    .nav-icon {
      background: rgba(31, 139, 255, 0.22);
      border-color: rgba(111, 204, 255, 0.3);
      color: #fff;
    }
  }
}

/* Asset-backed MDT sidebar pass */
.header-item {
  min-height: conv(58) !important;
  padding: 0 conv(12) !important;
  gap: conv(12) !important;
  border-radius: conv(11) !important;
  background:
    linear-gradient(135deg, rgba(30, 93, 170, 0.16), rgba(4, 13, 28, 0.78)),
    rgba(2, 10, 22, 0.66) !important;
  border-color: rgba(91, 190, 255, 0.17) !important;

  .nav-icon {
    width: conv(36) !important;
    height: conv(36) !important;
    min-width: conv(36) !important;
    border-radius: conv(10) !important;
    background:
      radial-gradient(circle, rgba(71, 180, 255, 0.22), rgba(71, 180, 255, 0.05) 58%, transparent),
      rgba(255, 255, 255, 0.04) !important;
    border-color: rgba(106, 203, 255, 0.2) !important;

    img {
      width: conv(23) !important;
      height: conv(23) !important;
      filter: drop-shadow(0 0 conv(11) rgba(84, 197, 255, 0.72)) !important;
    }
  }

  > span:not(.count):not(.nav-icon) {
    font-size: conv(14) !important;
    line-height: conv(16) !important;
    letter-spacing: 0.08em !important;
  }

  &::before {
    width: conv(5) !important;
    background: linear-gradient(180deg, #5ed0ff, #417bff) !important;
    box-shadow: 0 0 conv(18) rgba(72, 178, 255, 0.9) !important;
  }

  &:hover,
  &.active {
    background:
      linear-gradient(135deg, rgba(31, 139, 255, 0.26), rgba(83, 69, 255, 0.12)),
      rgba(2, 10, 22, 0.74) !important;
    border-color: rgba(114, 211, 255, 0.38) !important;
  }
}

/* PDF-layout correction pass: vertical tab buttons */
.header-item {
  height: conv(66) !important;
  min-height: conv(66) !important;
  padding: 0 conv(16) 0 conv(14) !important;
  border-radius: conv(5) !important;
  clip-path: polygon(0 0, calc(100% - #{conv(16)}) 0, 100% 50%, calc(100% - #{conv(16)}) 100%, 0 100%) !important;
  background: linear-gradient(90deg, rgba(80, 43, 180, 0.72), rgba(28, 93, 190, 0.62) 58%, rgba(16, 35, 82, 0.58)) !important;
  border: 1px solid rgba(127, 174, 255, 0.26) !important;
  box-shadow: inset 0 0 conv(22) rgba(110, 71, 255, 0.12), 0 0 conv(18) rgba(48, 102, 255, 0.12) !important;
  pointer-events: auto !important;
  cursor: pointer !important;
  opacity: 1 !important;

  .nav-icon {
    width: conv(42) !important;
    height: conv(42) !important;
    min-width: conv(42) !important;
    border-radius: conv(4) !important;
    border: 0 !important;
    background: rgba(255, 255, 255, 0.08) !important;
    box-shadow: inset 0 0 conv(18) rgba(255, 255, 255, 0.035) !important;

    img {
      width: conv(27) !important;
      height: conv(27) !important;
      filter: drop-shadow(0 0 conv(12) rgba(130, 220, 255, 0.75)) !important;
    }
  }

  > span:not(.count):not(.nav-icon) {
    font-size: conv(16) !important;
    line-height: conv(18) !important;
    font-weight: 900 !important;
    letter-spacing: 0.085em !important;
    color: rgba(255, 255, 255, 0.92) !important;
  }

  &::before {
    left: auto !important;
    right: conv(7) !important;
    top: 18% !important;
    width: conv(3) !important;
    height: 64% !important;
    opacity: 0 !important;
    background: #77d8ff !important;
    box-shadow: 0 0 conv(18) rgba(119, 216, 255, 0.9) !important;
  }

  &::after {
    background: linear-gradient(90deg, rgba(255, 255, 255, 0.08), rgba(121, 95, 255, 0.2), rgba(77, 176, 255, 0.1)) !important;
  }

  &:hover {
    transform: translateX(conv(7)) !important;
    border-color: rgba(153, 219, 255, 0.5) !important;
    filter: brightness(1.12) !important;
  }

  &.active {
    background: linear-gradient(90deg, rgba(108, 65, 235, 0.94), rgba(36, 131, 239, 0.82) 58%, rgba(27, 68, 156, 0.74)) !important;
    border-color: rgba(166, 226, 255, 0.62) !important;
    box-shadow: inset 0 0 conv(28) rgba(122, 88, 255, 0.2), 0 0 conv(26) rgba(65, 157, 255, 0.3) !important;

    &::before,
    &::after {
      opacity: 1 !important;
    }
  }

  .count {
    right: conv(24) !important;
    background: #ff4f68 !important;
    box-shadow: 0 0 conv(16) rgba(255, 79, 104, 0.45) !important;
  }
}
</style>
