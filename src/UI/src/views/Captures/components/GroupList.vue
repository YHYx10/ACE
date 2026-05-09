<template>
  <div class="group-list">
    <div class="header">
      <div class="nickname">{{ loc("captmenu_7") }}</div>
      <div class="rang">{{ loc("captmenu_8") }}</div>
    </div>
    <div class="group-list_list">
      <GroupListItem
        v-for="(item, index) in gangList"
        :key="item.id"
        :item="item"
        :index="index"
      />
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from "vuex";

import GroupListItem from "./GroupListItem";

export default {
  name: "GroupList",

  components: {
    GroupListItem,
  },

  computed: {
    ...mapState("captures", ["capturing", "gangList"]),
    ...mapGetters("localization", ["loc"]),
  },

  methods: {},
};
</script>

<style lang="scss" scoped>
$width: 1920;
$height: 1080;

@function conv($px, $direction: false) {
  @if $direction {
    @return ($px / $height) * 100vh;
  } @else {
    @return ($px / $width) * 100vw;
  }
}

.group-list {
	width: 100%;
  margin-top: conv(19, true);
  display: flex;
  flex-direction: column;

  .header {
    display: flex;
    align-items: center;
    padding-left: conv(40);
    margin-bottom: conv(19, true);

    div {
      font-family: "Akrobat";
      font-weight: 700;
      font-size: conv(14, true);
      line-height: conv(17, true);
      text-transform: uppercase;
      color: #ffffff;
    }

    .nickname {
      width: conv(198);
      margin-right: conv(45);
    }

    .rang {
		width: conv(150);
    }
  }

  &_list {
    padding-right: conv(15);
    overflow-y: scroll;
    max-height: conv(522, true);
	
    &::-webkit-scrollbar-track {
      border-radius: 0.25rem;
    }
    &::-webkit-scrollbar {
      width: 0.2rem;
    }
    &::-webkit-scrollbar-thumb {
      border-radius: 0.25rem;
      background: rgba(255, 255, 255, 0.15);
    }
  }
}
</style>