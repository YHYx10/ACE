<template>
  <TabsTemplate>
    <Change
      @change="(status) => changeGender(status)"
      :btnFirst="'Male'"
      :btnSecond="'Female'"
      :gender="gender"
    />
    <Input
      className="form__name"
      title="The name of the character"
      :placeholder="'The name of the character'"
      :val="fitrstNameModel"
      @change="(val) => (fitrstNameModel = val)"
    />
    <Input
      className="form__subname"
      title="The last name of the character"
      :placeholder="'The last name of the character'"
      :val="lastNameModel"
      @change="(val) => (lastNameModel = val)"
    />
  </TabsTemplate>
</template>

<script>
import { mapMutations, mapState } from "vuex";
import TabsTemplate from "./TabsTemplate.vue";
import Change from "./tools/Change.vue";
import Input from "./tools/Input.vue";

export default {
  name: "Info",
  components: { TabsTemplate, Change, Input },
  props: {
    itemData: Object,
  },
  computed: {
    ...mapState("customization", [
      "gender",
      "firstName",
      "lastName",
      "age",
      "firstCreate",
    ]),
    fitrstNameModel: {
      get() {
        return this.firstName;
      },
      set(value) {
        this.updateFirstName(value);
      },
    },
    lastNameModel: {
      get() {
        return this.lastName;
      },
      set(value) {
        this.updateLastName(value);
      },
    },
  },
  methods: {
    ...mapMutations("customization", [
      "updateFirstName",
      "updateLastName",
      "updateAge",
      "changeGender",
    ]),
  },
};
</script>