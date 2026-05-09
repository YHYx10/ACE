<template>
  <div class="chat chat-v2" v-if="isShow" :class="{ 'chat-hidden': opacity }">
    <div class="chat-body">
      <div class="chat-messages" ref="list">
        <div
          class="chat-message"
          :class="[
            `chat-message--${messageKind(message)}`,
            { 'chat-message--old': index < messages.length - 8 },
          ]"
          v-for="(message, index) in messages"
          :key="index"
        >
          <img
            v-if="messageIcon(message)"
            class="chat-message__icon"
            :src="messageIcon(message)"
            alt=""
          />
          <span class="chat-message__content" v-html="messageHtml(message)"></span>
        </div>
      </div>
      <transition name="chat-compose">
        <div class="chat-compose" v-show="enabled">
          <div class="chat-channels">
              <button
              class="chat-channel"
              v-for="(btn, index) in visibleButtons"
              :key="index"
              :class="{ active: current == index }"
              type="button"
              @click="selectChatType(index)"
            >
              {{ loc(btn.name) }}
            </button>
          </div>

          <div class="chat-input">
            <input
              type="text"
              v-model="input"
              @change="onChnge"
              :disabled="!enabled"
              ref="text"
              placeholder="Enter your message..."
            />
            <button class="chat-send" type="button" @click="onChnge">
              <img src="/img/chat-v2/send.svg" alt="" />
            </button>
          </div>

          <div class="chat-actions">
            <span class="chat-actions__label">Roleplay actions</span>
            <button
              v-for="action in chatActions"
              :key="action"
              type="button"
              @click="applyChatAction(action)"
            >
              /{{ action }}
            </button>
          </div>
        </div>
      </transition>
    </div>
  </div>
</template>
<script>
import { mapGetters, mapState } from "vuex";

export default {
  watch: {
    messages(old, news) {
      console.log(old);
      this.messages = news;
    },
  },
  data() {
    return {
      messages: [],
      history: [],
      buttons: [
        { name: "IC", preffix: "chat " },
        { name: "OOC", preffix: "b " },
        { name: "RP", preffix: "f ", require: "faction" },
        { name: "NON-RP", preffix: "fb", require: "faction" },
        { name: "Family", preffix: "fam ", require: "family" },
      ],
      membership: {
        hasFaction: false,
        hasFamily: false,
        factionId: 0,
        factionName: "",
        familyIconId: null,
      },
      chatActions: ["me", "do", "try", "todo"],
      input: "",
      current: 0,
      historyLength: 150,
      enabled: true /* false */,
      active: true,
      isShow: true,
      time: 20000,
      lastMsg: 0,
      opacity: false,
      interval: null,
      index: -1,
    };
  },
  computed: {
    ...mapGetters("localization", ["loc"]),
    ...mapState("familyMenu/controlPage", ["chatOptions"]),
    ...mapState(["currentPage"]),
    hasReliableFamilyState() {
      return this.membership.hasFamily === true;
    },
    hasReliableFactionState() {
      return this.membership.hasFaction === true;
    },
    visibleButtons() {
      return this.buttons.filter((button) => {
        if (button.require === "family") return this.hasReliableFamilyState;
        if (button.require === "faction") return this.hasReliableFactionState;
        return true;
      });
    },
  },
  methods: {
    selectChatType(index) {
      this.current = index;
      this.focus();
    },
    nextChatType() {
      this.current++;
      if (this.current >= this.visibleButtons.length) this.current = 0;
      this.focus();
    },
    applyChatAction(action) {
      this.input = `/${action} `;
      this.focus();
    },
    onChnge() {
      //this.resetOpacity();
      this.index = -1;
      this.input = this.input.replace(/</g, "");
      this.input = this.input.replace(/\]/g, "");
      this.input = this.input.replace(/\\/g, "");
      this.input = this.input.replace(/&/g, "and");
      this.input = this.input.replace(/#/g, "");
      if (this.input.length > 0) {
        this.history.push(this.input);
        if (this.input[0] === "/") {
          const value = this.input.substr(1);
          if (value.length > 0) window.mp.invoke("command", value);
        } else {
          const channel = this.visibleButtons[this.current] || this.visibleButtons[0];
          window.mp.invoke(
            "command",
            `${channel.preffix} ${this.input}`
          );
        }
      }
      this.enableChatInput(false);
    },
    tick() {
      if (!this.opacity && !this.enabled && this.lastMsg < Date.now())
        this.opacity = true;
    },
    getColor(key) {
      switch (key) {
        case "white":
          return "#FFFFFF";
        case "turquoise":
          return "#40FFD1";
        case "red":
          return "#FE5353";
        case "blue":
          return "#40A3FF";
        case "green":
          return "#3BEA62";
        case "orange":
          return "#FF891D";
        case "darkred":
          return "#EA3B3B";
        case "yellow":
          return "#EABE3B";
        case "citric":
          return "#FFDB1E";
        case "me":
          return "#F060A5";
        case "do":
          return "#EC328B";
        case "true":
          return "#74F060";
        case "false":
          return "#FFFFFF";
        case "warning":
          return "#EA3B3B";
        case "grey":
          return "#888888";
        case "greengo":
          return "#B6D300";
        case "ooc":
          return "#CBCBCB";
        default:
          return "#FFFFFF";
      }
    },
    getFamilyColor() {
      return this.chatOptions.currentColor;
    },
    getFamilyIcon() {
      return this.chatOptions.currentIcon;
    },
    messageHtml(message) {
      return typeof message === "object" ? message.html : message;
    },
    messageKind(message) {
      return typeof message === "object" ? message.kind : "default";
    },
    messageIcon(message) {
      return typeof message === "object" ? message.icon : null;
    },
    iconPath(name) {
      return name ? `img/chat-v2/${name}` : null;
    },
    iconForKind(kind) {
      switch (kind) {
        case "system":
          return this.iconPath("endless.svg");
        case "error":
          return this.iconPath("reach.svg");
        case "family":
          return this.iconForFamily();
        case "faction":
        case "faction-rp":
        case "faction-nonrp":
          return this.iconForFaction();
        case "police":
          return this.iconPath("ico-1.svg");
        case "fib":
          return this.iconPath("Fbi.svg");
        case "medic":
          return this.iconPath("medicine.svg");
        case "army":
          return this.iconPath("military.svg");
        case "government":
          return this.iconPath("parliament.svg");
        case "mafia":
          return this.iconPath("mafia1.svg");
        case "gang":
          return this.iconPath("band1.svg");
        case "smi":
          return this.iconPath("editing.svg");
        case "megaphone":
          return this.iconPath("megaphone.svg");
        case "phone":
          return this.iconPath("phone.svg");
        case "sms":
          return this.iconPath("sms.svg");
        default:
          return null;
      }
    },
    iconForFaction() {
      const factionName = String(this.membership.factionName || "").toLowerCase();
      const factionId = Number(this.membership.factionId) || 0;

      if (factionName.includes("vagos")) return this.iconPath("band1.svg");
      if (factionName.includes("families")) return this.iconPath("band2.svg");
      if (factionName.includes("marabunta")) return this.iconPath("band3.svg");
      if (factionName.includes("ballas")) return this.iconPath("band4.svg");
      if (factionName.includes("bloods")) return this.iconPath("band5.svg");
      if (factionName.includes("fib")) return this.iconPath("Fbi.svg");
      if (factionName.includes("police") || factionName.includes("lspd")) return this.iconPath("ico-1.svg");
      if (factionName.includes("ems") || factionName.includes("medic")) return this.iconPath("medicine.svg");
      if (factionName.includes("ng") || factionName.includes("army")) return this.iconPath("military.svg");
      if (factionName.includes("gov") || factionName.includes("government")) return this.iconPath("parliament.svg");

      const iconsByFactionId = {
        1: "band2.svg",
        2: "band4.svg",
        3: "band1.svg",
        4: "band3.svg",
        5: "band5.svg",
        6: "parliament.svg",
        7: "ico-1.svg",
        8: "medicine.svg",
        9: "Fbi.svg",
        14: "military.svg",
      };

      return this.iconPath(iconsByFactionId[factionId] || "radio.svg");
    },
    iconForFamily() {
      const familyIconId = Number(this.membership.familyIconId);
      const familyIcons = [
        "fam.svg",
        "fam0.svg",
        "fam1.svg",
        "fam2.svg",
        "fam3.svg",
        "fam4.svg",
        "fam5.svg",
        "fam6.svg",
        "fam7.svg",
        "fam8.svg",
        "fam9.svg",
      ];

      return this.iconPath(familyIcons[familyIconId - 1] || "fam.svg");
    },
    iconForGovernmentTag(tag) {
      switch (tag) {
        case "LSPD":
          return this.iconForKind("police");
        case "FIB":
          return this.iconForKind("fib");
        case "EMS":
          return this.iconForKind("medic");
        case "ARMY":
          return this.iconForKind("army");
        case "NEWS":
          return this.iconForKind("smi");
        default:
          return this.iconForKind("government");
      }
    },
    pushMessage(kind, html, icon = null) {
      this.messages.push({ kind, html, icon: icon || this.iconForKind(kind) });
    },
    isWrappedOocMessage(message) {
      return /^\s*\(\(/.test(message) && /\)\)\s*$/.test(message);
    },
    normalizeNonRpText(message) {
      const text = String(message || "").trim();
      return this.isWrappedOocMessage(text) ? text : `(( ${text} ))`;
    },
    setMembership(data = {}) {
      this.membership = {
        hasFaction: data.hasFaction === true,
        hasFamily: data.hasFamily === true,
        factionId: Number(data.factionId) || 0,
        factionName: data.factionName || "",
        familyIconId: data.familyIconId,
      };
      if (this.current >= this.visibleButtons.length) this.current = 0;
    },
    push(type, msg, id, from, toId, to, friend) {
      let tag;
      /* console.log(this.messages) */
      if (msg == undefined) {
        if (type === "RAGE MP: Connection lost. Reconnecting...") {
          window.mp.trigger("onConnectionLost");
        }
        this.pushMessage("system", `
                    <span class="chat-line chat-line--system"><span class="chat-line__icon"></span><span>${this.loc(
          type
        )}</span></span>
                `);
      } else {
        switch (type) {
          case 0: //Ð¾Ð±Ñ‹Ñ‡Ð½Ñ‹Ð¹ Ñ‡Ð°Ñ‚ +
            if (!friend) from = this.loc("chat_7");
            this.pushMessage(
              "ic",
              `<span class="chat-line chat-line--ic"><span class="chat-line__icon"></span><span><b class="chat-line__name">${from}</b> <span class="chat-line__id">[${id}]</span>: ${this.loc(msg)}</span></span>`
            );
            break;
          case 1: //Scream +
            if (!friend) from = this.loc("chat_7");
            this.pushMessage("ic", `
                            <span style="color:${this.getColor(
                              "white"
                            )};">${from} [${id}] ${this.loc(
              "chat_4"
            )}: ${this.loc(msg)}</span>
                        `);
            break;
          case 2: //of chat  +
            if (!friend) from = this.loc("chat_7");
            this.pushMessage("ooc", `
                            <span class="chat-line chat-line--ooc"><span class="chat-line__icon"></span><span><b class="chat-line__name">${from}</b> <span class="chat-line__id">[${id}]</span>: (( ${this.loc(msg)} ))</span></span>
                        `);
            break;
          case 3: //ÑÐ¾Ð¾Ð±Ñ‰ÐµÐ½Ð¸Ðµ Ð°Ð´Ð¼Ð¸Ð½Ð° +
            this.pushMessage("admin", `
                            <span style="color:${this.getColor(
                              "white"
                            )};"><span style="color:${this.getColor(
              "turquoise"
            )};">[A] ${from} [${id}]:</span> ${this.loc(msg)}</span>
                        `);
            break;
          case 4: //Ð¾Ñ‚Ð²ÐµÑ‚ Ð¾Ñ‚ Ð°Ð´Ð¼Ð¸Ð½Ð¸ÑÑ‚Ñ€Ð°Ñ‚Ð¾Ñ€Ð°+
            this.pushMessage("admin", `
                            <span style="color:${this.getColor(
                              "white"
                            )};"><span style="color:${this.getColor(
              "red"
            )};">> ${this.loc("chat_5")} ${from}:</span> ${this.loc(msg)}</span>
                        `);
            break;
          case 5: //Ñ‡Ð°Ñ‚ Ñ„Ñ€Ð°ÐºÑ†Ð¸Ð¸+
              {
                const factionText = this.loc(msg);
                const isNonRp = this.isWrappedOocMessage(factionText);
                this.pushMessage(isNonRp ? "faction-nonrp" : "faction-rp", `
                                  <span class="chat-line ${isNonRp ? "chat-line--faction-nonrp" : "chat-line--faction-rp"}"><span class="chat-line__icon"></span><span><b class="chat-line__name">${from}</b> <span class="chat-line__id">[${id}]</span>: ${isNonRp ? this.normalizeNonRpText(factionText) : factionText}</span></span>
                              `, this.iconForFaction());
              }

            break;
          case 6: //Ñ‡Ð°Ñ‚ ÑÐµÐ¼ÑŒÐ¸+
            this.pushMessage("family", `
                            <span class="chat-line chat-line--family"><span class="chat-line__icon"></span><span><b class="chat-line__name">${from}</b> <span class="chat-line__id">[${id}]</span>: ${this.loc(msg)}</span></span>
                        `, this.iconForFamily());
            break;
          case 7: //Ñ‡Ð°Ñ‚ Ð³Ð¾Ñ ÑÑ‚Ñ€ÑƒÐºÑ‚ÑƒÑ€Ñ‹+
            switch (toId) {
              case 7:
                tag = "LSPD";
                break;
              case 8:
                tag = "EMS";
                break;
              case 9:
                tag = "FIB";
                break;
              case 14:
                tag = "ARMY";
                break;
              case 15:
                tag = "NEWS";
                break;
              default:
                tag = "GOV";
                break;
            }

            /* <span class="global-chat__icon global-chat__icon-gos"></span> */
            this.pushMessage("government", `
                            <span style="color:${this.getColor(
                              "white"
                            )};" class="gov-chat">
                                <span>Dep</span>
                                <span style="color:${this.getColor("orange")};">
                                    (${tag}) ${from} [${id}]:
                                </span>
                                ${this.loc(msg).replace("_", " ")}
                            </span>
                        `, this.iconForGovernmentTag(tag));
            break;
          case 8: //Ð±Ð°Ð½/ÐºÐ¸Ðº/Ð¼ÑƒÑ‚ Ð¾Ñ‚ Ð°Ð´Ð¼Ð¸Ð½Ð°  +
            this.pushMessage("admin", `
                            <span style="color:${this.getColor(
                              "white"
                            )};" class="admin"><span>ADMIN</span>${this.loc(
              msg
            )}</span>
                        `);
            break;
          case 9: //Ð¾Ñ‚Ð²ÐµÑ‚ Ð°Ð´Ð¼Ð¸Ð½Ð¸ÑÑ‚Ñ€Ð°Ñ‚Ð¾Ñ€Ð° Ð¸Ð³Ñ€Ð¾ÐºÑƒ (Ð´Ð»Ñ Ð°Ð´Ð¼Ð¸Ð½Ð°)
            this.pushMessage("admin", `
                            <span style="color:${this.getColor(
                              "white"
                            )};" class="answer">
                                <span>Administrator</span>
                                <span>
                                <span style="color:${this.getColor("yellow")};">
                                    ${from} [${id}] > ${to} [${toId}]:
                                </span> 
                                ${this.loc(msg)}
                                </span>
                            </span>
                        `);
            break;
          case 10: //me+
            if (!friend) from = this.loc("chat_7");
            this.pushMessage("me", `
                            <span class="chat-line chat-line--me"><span class="chat-line__icon"></span><span><b class="chat-line__name">${from}</b> <span class="chat-line__id">[${toId}]</span> ${this.loc(msg)}</span></span> 
                        `);
            this.addActionTo(id, msg);
            break;
          case 11: //do+
            if (!friend) from = this.loc("chat_7") + `[${id}]`;
            this.pushMessage("do", `
                            <span class="chat-line chat-line--do"><span class="chat-line__icon"></span><span>${this.loc(msg)} <b class="chat-line__name">${from}</b> <span class="chat-line__id">[${toId}]</span></span></span> 
                        `);
            this.addActionTo(id, msg);
            break;
          case 12: //try
            msg =
              msg.indexOf("False") === -1
                ? msg.replace(
                    "True",
                    `: <span style="color:${this.getColor("true")};">${this.loc(
                      "Core_298"
                    )}</span>`
                  )
                : msg.replace(
                    "False",
                    `: <span style="color:${this.getColor(
                      "false"
                    )};">${this.loc("Core_299")}</span>`
                  );
            if (!friend) from = this.loc("chat_7") + `[${id}]`;
            this.pushMessage("try", `
                            <span class="chat-line chat-line--try"><span class="chat-line__icon"></span><span><b class="chat-line__name">${from}</b> <span class="chat-line__id">[${id}]</span> ${this.loc(msg)}</span></span> 
                        `);
            break;
          case 13: //ÑÐ¾Ð¾Ð±Ñ‰ÐµÐ½Ð¸Ðµ Ð² Ð³Ð¾Ð² Ð¸Ð»Ð¸ Ð² Ð¾Ñ€Ð°Ð»Ð¾
            switch (toId) {
              case 7:
                tag = "LSPD";
                break;
              case 8:
                tag = "EMS";
                break;
              case 9:
                tag = "FIB";
                break;
              case 14:
                tag = "ARMY";
                break;
              case 15:
                tag = "NEWS";
                break;
              default:
                tag = "GOV";
                break;
            }
            this.pushMessage("government", `
                            <span style="color:${this.getColor(
                              "yellow"
                            )};" class="gov-message"><span>${tag}</span>${from}: ${this.loc(
              msg
            )}</span>
                        `, this.iconForGovernmentTag(tag));
            break;
          case 14: //Ð½Ð¾Ð²Ð¾ÑÑ‚Ð¸ Ð¾Ñ‚ Ð°Ð´Ð¼Ð¸Ð½Ð°
            this.pushMessage("admin", `
							<span style="color:${this.getColor("citric")};">${this.loc(
              "chat_5"
            )} ${from}: ${this.loc(msg)}</span>	
						`);
            break;
          case 15: //Ð¼ÐµÐ³Ð°Ñ„Ð¾Ð½ Ð´Ð»Ñ Ð³Ð¾Ñ ÑÐ¾Ñ‚Ñ€ÑƒÐ´Ð½Ð¸ÐºÐ¾Ð²
            /* <span class="global-chat__icon global-chat__icon-megaphone"></span> */
            this.pushMessage("megaphone", `
                            <span style="color:${this.getColor(
                              "white"
                            )};" class="megaphone">
                                <span>Megaphone</span>   
                                <span>
                                    <span style="color:${this.getColor(
                                      "yellow"
                                    )};">
                                        ${from} [${id}]:
                                    </span> 
                                    ${this.loc(msg)}
                                </span>
                            </span>
                        `);
            break;
          case 16:
            this.pushMessage("smi", `
                            <span class="ads">
                                <span>Medien</span>
                                <div>
                                    <span>${msg}</span>
                                    <div>
                                        <div class="global-chat__icon global-chat__icon-circle-msg"></div>
                                        <div class="global-chat__icon global-chat__icon-circle-tel"></div> 
                                        <span>${this.loc(
                                          "chat_6"
                                        )} ${from}</span>
                                    </div>
                                </div>
                            </span>
                            
                        `);
            /* <span style="color:${this.getColor("greengo")};">
                                ${from}: ${id}
                            </span>
                            <div class="sender-wrap">
                                <div class="global-chat__icon global-chat__icon-circle global-chat__icon-circle-call"></div>
                                <div class="global-chat__icon global-chat__icon-circle global-chat__icon-circle-message"></div> 
                                <div class="sender__nickname-tittle">${this.loc(
                                  "chat_6"
                                )}: </div>
                                <div class="sender__nickname">${msg}</div>
                                <div class="sender__nickname-phone">${
                                  toId == -1 ? "" : `tel: ${toId}`
                                }</div>
                            </div> */
            break;
          case 17:
            this.pushMessage("error", `
                            <span style="color:${this.getColor(
                              "warning"
                            )};">${this.loc(msg)}</span>
                        `);
            break;
          case 18:
            this.pushMessage("system", `
                            <span style="color:${this.getColor(
                              "white"
                            )};">${this.loc(msg)}</span>
                        `);
            break;
        }
      }
      if (this.historyLength < this.messages.length) this.messages.shift();
      //this.resetOpacity();
      this.scrollBottom();
      /* console.log(this.messages)
            console.log('new') */
      return id;
    },
    scrollBottom() {
      setTimeout(() => {
        const list = this.$refs.list;
        if (list) list.scrollTop = list.scrollHeight;
      }, 0);
    },
    clear() {
      this.messages = [];
      if (this.enabled) {
        window.setTimeout(() => {
          this.$refs.text.focus();
        }, 0);
      }
    },
    activate(toggle) {
      this.active = toggle;
      if (!toggle && this.enabled) this.enableChatInput(false);
    },
    focus() {
      window.setTimeout(() => {
        if (this.$refs.text) this.$refs.text.focus();
      }, 0);
    },
    show(toggle) {
      this.isShow = toggle;
      this.enabled = false;
      if (toggle) this.scrollBottom();
    },
    enableChatInput(enable) {
      if (this.active) {
        window.mp.invoke("focus", enable);
        window.mp.invoke("setTypingInChatState", enable);
        this.enabled = enable;
        if (enable) this.focus();
        else window.mp.trigger("cahat:api:disable");
      } else {
        window.mp.trigger("cahat:api:disable");
        this.enabled = false;
      }
      this.input = "";
    },
    resetOpacity() {
      this.lastMsg = Date.now() + this.time;
      this.opacity = false;
    },
    addActionTo(id, msg) {
      window.mp.trigger("tag:add:action", id, this.loc(msg));
    },
    keyUp(e) {
      e.preventDefault();
      switch (e.keyCode) {
        // case 84: //t
        //     if(this.currentPage === undefined || this.currentPage === ""){
        //         if(!this.enabled && this.active)
        //             this.enableChatInput(true);
        //     }

        //     break;

        case 9: //tab
          if (this.enabled) this.nextChatType();
          this.scrollBottom();
          break;

        case 13: //enter
          if (this.enabled) this.onChnge();
          break;

        case 38: //up
          if (!this.enabled) return;
          if (this.history.length === 0) return;
          this.index -= 1;
          if (this.index < 0) this.index = this.history.length - 1;
          this.input = this.history[this.index];
          break;

        case 40: //down
          if (!this.enabled) return;
          if (this.history.length === 0) return;
          this.index += 1;
          if (this.index >= this.history.length) this.index = 0;
          this.input = this.history[this.index];
          break;
        default:
          break;
      }
    },
  },
  mounted() {
    window.chatAPI = {
      push: this.push,
      clear: this.clear,
      activate: this.activate,
      show: this.show,
      enable: this.enableChatInput,
      setMembership: this.setMembership,
    };
    document.addEventListener("keyup", this.keyUp);
  },
  beforeDestroy() {
    document.removeEventListener("keyup", this.keyUp);
  },
};
</script>

<style lang="scss" scoped>
.chat {
  position: fixed;
  left: 1.35rem;
  top: 1.15rem;
  width: 31.5rem;
  pointer-events: none;
  transition: opacity 0.35s ease;
  font-family: "Akrobat", sans-serif;
  color: #fff;

  &-body {
    width: 100%;
    height: 36vh;
    min-height: 18rem;
    max-height: 27rem;
    position: relative;
    display: flex;
    flex-direction: column;
    justify-content: flex-end;
    align-items: stretch;

  }

  &-messages {
    width: 100%;
    max-height: 100%;
    overflow: hidden auto;
    padding: 0 0.35rem 0.3rem 0;
    mask-image: linear-gradient(to bottom, transparent 0, #000 2.7rem, #000 calc(100% - 0.25rem));
    scroll-behavior: smooth;
    position: relative;
    z-index: 1;

    &::-webkit-scrollbar {
      width: 0.25rem;
    }

    &::-webkit-scrollbar-thumb {
      border-radius: 999px;
      background: rgba(255, 255, 255, 0.18);
    }

    &::-webkit-scrollbar-track {
      background: transparent;
    }
  }

  &-message {
    width: fit-content;
    max-width: 100%;
    display: flex;
    align-items: flex-start;
    gap: 0.38rem;
    color: #fff;
    margin-top: 0.22rem;
    padding: 0.15rem 0.38rem 0.15rem 0;
    font-size: 0.82rem;
    font-weight: 600;
    line-height: 1.04rem;
    letter-spacing: 0.015em;
    word-break: break-word;
    opacity: 0.98;
    transition: opacity 0.2s ease, transform 0.2s ease;

    &__icon {
      width: 0.9rem;
      min-width: 0.9rem;
      height: 0.9rem;
      margin-top: 0.08rem;
      display: block;
      object-fit: contain;
      filter: drop-shadow(0 0 0.28rem rgba(255, 255, 255, 0.28));
      opacity: 0.92;
      pointer-events: none;
    }

    &__content {
      display: inline-block;
      min-width: 0;
    }

    &__content .chat-line__icon {
      display: none;
    }

    &--old {
      opacity: 0.9;
      text-shadow:
        0 0.08rem 0.16rem rgba(0, 0, 0, 0.82),
        0 0 0.34rem rgba(0, 0, 0, 0.62);
    }

    &--ic {
      color: #ffffff;
    }

    &--ooc {
      color: rgba(224, 224, 224, 0.78);
    }

    &--me {
      color: #f060a5;
      text-shadow: 0 0 0.45rem rgba(240, 96, 165, 0.22);
    }

    &--do {
      color: #48a8ff;
      text-shadow: 0 0 0.45rem rgba(72, 168, 255, 0.22);
    }

    &--try {
      color: #ffd35a;
      text-shadow: 0 0 0.45rem rgba(255, 211, 90, 0.22);
    }

    &--admin,
    &--error {
      color: #ff5c5c;
      text-shadow: 0 0 0.55rem rgba(255, 69, 69, 0.28);
    }

    &--error .chat-message__icon {
      filter: drop-shadow(0 0 0.42rem rgba(255, 69, 69, 0.42));
    }

    &--faction {
      color: #52a8ff;
    }

    &--faction-rp {
      color: #ffffff;
    }

    &--faction-nonrp {
      color: rgba(224, 224, 224, 0.78);
    }

    &--family {
      color: #77ff9d;
    }

    &--faction .chat-message__icon,
    &--faction-rp .chat-message__icon,
    &--faction-nonrp .chat-message__icon,
    &--government .chat-message__icon,
    &--fib .chat-message__icon,
    &--police .chat-message__icon,
    &--medic .chat-message__icon,
    &--army .chat-message__icon,
    &--smi .chat-message__icon,
    &--megaphone .chat-message__icon {
      filter: drop-shadow(0 0 0.38rem rgba(82, 168, 255, 0.35));
    }

    &--family .chat-message__icon {
      filter: drop-shadow(0 0 0.38rem rgba(119, 255, 157, 0.35));
    }

    &--system {
      color: rgba(255, 255, 255, 0.72);
    }
  }

  &-compose {
    width: 28.25rem;
    margin-top: 0.78rem;
    pointer-events: auto;
  }

  &-channels {
    display: flex;
    align-items: center;
    gap: 0.3rem;
    margin-bottom: 0.34rem;
  }

  &-channel,
  &-actions button {
    height: 1.55rem;
    padding: 0 0.66rem;
    border: 0;
    border-radius: 0.2rem;
    background: rgba(11, 13, 20, 0.72);
    color: rgba(255, 255, 255, 0.74);
    font-size: 0.66rem;
    font-weight: 800;
    line-height: 1;
    letter-spacing: 0.045em;
    text-transform: uppercase;
    box-shadow: inset 0 0 0 1px rgba(255, 255, 255, 0.04), 0 0.6rem 1.25rem rgba(0, 0, 0, 0.28);
    transition: color 0.18s ease, background 0.18s ease, box-shadow 0.18s ease, transform 0.18s ease;

    &:hover,
    &.active {
      background: linear-gradient(180deg, #ffec7a 0%, #f2bd39 100%);
      color: #1b1620;
      box-shadow: 0 0 1.2rem rgba(255, 214, 76, 0.22), inset 0 0 0 1px rgba(255, 255, 255, 0.22);
      transform: translateY(-0.03rem);
    }
  }

  &-input {
    position: relative;
    width: 100%;
    height: 2.55rem;
    border-radius: 0.24rem;
    background: linear-gradient(90deg, rgba(9, 11, 17, 0.88), rgba(9, 11, 17, 0.64));
    box-shadow: 0 0.75rem 1.8rem rgba(0, 0, 0, 0.32), inset 0 0 0 1px rgba(255, 255, 255, 0.055);

    input {
      width: 100%;
      height: 100%;
      color: #fff;
      background: transparent;
      padding: 0 2.95rem 0 0.86rem;
      font-size: 0.84rem;
      font-weight: 700;
      letter-spacing: 0.02em;
      border: 0;
      outline: 0;

      &::placeholder {
        color: rgba(255, 255, 255, 0.42);
        font-size: 0.76rem;
        font-weight: 600;
      }
    }
  }

  &-send {
    position: absolute;
    top: 50%;
    right: 0.42rem;
    width: 1.78rem;
    height: 1.78rem;
    display: flex;
    align-items: center;
    justify-content: center;
    border: 0;
    border-radius: 0.18rem;
    background: linear-gradient(180deg, #ffec7a 0%, #f2bd39 100%);
    box-shadow: 0 0 1rem rgba(255, 214, 76, 0.26);
    transform: translateY(-50%);
    transition: filter 0.18s ease, transform 0.18s ease;

    img {
      width: 0.78rem;
      height: 0.78rem;
      display: block;
      filter: brightness(0) saturate(100%);
    }

    &:hover {
      filter: brightness(1.08);
      transform: translateY(-50%) scale(1.03);
    }
  }

  &-actions {
    display: flex;
    align-items: center;
    gap: 0.3rem;
    margin-top: 0.38rem;

    &__label {
      margin-right: 0.2rem;
      color: rgba(255, 255, 255, 0.38);
      font-size: 0.62rem;
      font-weight: 800;
      letter-spacing: 0.08em;
      text-transform: uppercase;
      text-shadow: 0 0 0.4rem rgba(0, 0, 0, 0.65);
    }

    button {
      height: 1.35rem;
      padding: 0 0.52rem;
      font-size: 0.6rem;
      background: rgba(14, 17, 26, 0.62);
    }
  }

  &-hidden {
    opacity: 0.16;
  }
}

.chat-compose-enter-active,
.chat-compose-leave-active {
  transition: opacity 0.22s ease, transform 0.22s ease;
}

.chat-compose-enter,
.chat-compose-leave-to {
  opacity: 0;
  transform: translateY(0.35rem);
}
</style>

<style lang="scss">
.chat {
  &-message {
    &,
    span,
    div {
      text-shadow: 0 0 0.25rem rgba(0, 0, 0, 1);
    }

    span {
      font-family: "Akrobat";
      &.admin {
        display: flex;
        align-items: center;

        span {
          background: rgba(71, 44, 132, 0.3);
          border: 0.053rem solid rgba(71, 44, 132, 0.5);
          border-radius: 0.211rem;
          display: flex;
          align-items: center;
          justify-content: center;
          padding: 0.368rem 0.632rem 0.421rem 0.684rem;
          margin-right: 0.526rem;
          font-weight: 700;
          font-size: 0.737rem;
          line-height: 0.895rem;
          color: #ffffff;
        }
      }

      &.answer {
        display: flex;
        align-items: center;

        & > span:first-child {
          background: rgba(#eabe3b, 0.3);
          border: 0.053rem solid rgba(#eabe3b, 0.5);
          border-radius: 0.211rem;
          display: flex;
          align-items: center;
          justify-content: center;
          padding: 0.368rem 0.632rem 0.421rem 0.684rem;
          margin-right: 0.526rem;
          font-weight: 700;
          font-size: 0.737rem;
          line-height: 0.895rem;
          color: #ffffff;
        }
      }

      &.gov-message {
        display: flex;
        align-items: center;
        color: white !important;

        & > span:first-child {
          background: rgba(#0a285a, 0.3);
          border: 0.053rem solid rgba(#0a285a, 0.5);
          border-radius: 0.211rem;
          display: flex;
          align-items: center;
          justify-content: center;
          padding: 0.368rem 0.632rem 0.421rem 0.684rem;
          margin-right: 0.526rem;
          font-weight: 700;
          font-size: 0.737rem;
          line-height: 0.895rem;
          color: #ffffff;
        }
      }

      &.gov-chat {
        display: flex;
        align-items: center;
        color: white !important;

        & > span:first-child {
          background: rgba(#eabe3b, 0.3);
          border: 0.053rem solid rgba(#eabe3b, 0.5);
          border-radius: 0.211rem;
          display: flex;
          align-items: center;
          justify-content: center;
          padding: 0.368rem 0.632rem 0.421rem 0.684rem;
          margin-right: 0.526rem;
          font-weight: 700;
          font-size: 0.737rem;
          line-height: 0.895rem;
          color: #ffffff;
        }
      }

      &.megaphone {
        display: flex;
        align-items: center;

        & > span:first-child {
          background: rgba(#eabe3b, 0.3);
          border: 0.053rem solid rgba(#eabe3b, 0.5);
          border-radius: 0.211rem;
          display: flex;
          align-items: center;
          justify-content: center;
          padding: 0.368rem 0.632rem 0.421rem 0.684rem;
          margin-right: 0.526rem;
          font-weight: 700;
          font-size: 0.737rem;
          line-height: 0.895rem;
          color: #ffffff;
        }
      }

      &.ads {
        display: flex;
        align-items: center;

        & > span:first-child {
          background: rgba(255, 199, 0, 0.3);
          border: 1px solid rgba(255, 199, 0, 0.5);
          border-radius: 0.211rem;
          display: flex;
          align-items: center;
          justify-content: center;
          padding: 0.368rem 0.632rem 0.421rem 0.684rem;
          margin-right: 0.526rem;
          font-weight: 700;
          font-size: 0.737rem;
          line-height: 0.895rem;
          color: #ffffff;
        }

        & > div {
          display: flex;
          flex-direction: column;

          & > span {
            font-weight: 700;
            font-size: 0.842rem;
            line-height: 1rem;
          }

          & > div {
            display: flex;
            align-items: center;
            font-weight: 600;
            font-size: 0.737rem;
            line-height: 0.895rem;

            span {
              display: block;
            }
          }
        }
      }
    }
  }
}

.chat-line {
  display: inline-flex;
  align-items: baseline;
  gap: 0.22rem;
  color: inherit;

  &__icon {
    width: 0.42rem;
    height: 0.42rem;
    margin-right: 0.08rem;
    border-radius: 50%;
    background: currentColor;
    box-shadow: 0 0 0.45rem currentColor;
    opacity: 0.55;
    transform: translateY(-0.02rem);
  }

  &__name {
    color: rgba(255, 255, 255, 0.96);
    font-weight: 900;
    letter-spacing: 0.02em;
  }

  &__id {
    color: rgba(255, 255, 255, 0.46);
    font-weight: 700;
  }

  &--ooc {
    color: rgba(224, 224, 224, 0.78);

    .chat-line__name {
      color: rgba(255, 255, 255, 0.76);
    }
  }

  &--me {
    color: #f060a5;
  }

  &--do {
    color: #48a8ff;
  }

  &--try {
    color: #ffd35a;
  }

  &--faction-rp {
    color: #ffffff;
  }

  &--faction-nonrp {
    color: rgba(224, 224, 224, 0.78);

    .chat-line__name {
      color: rgba(255, 255, 255, 0.76);
    }
  }

  &--family {
    color: #77ff9d;
  }

  &--system {
    color: rgba(255, 255, 255, 0.7);

    .chat-line__icon {
      opacity: 0.28;
    }
  }
}

.global-chat__icon {
  width: 1.3rem;
  min-width: 1.3rem;
  height: 1.3rem;
  margin-right: 0.3rem;
  margin-bottom: -0.3rem;
  display: inline-block;
  position: relative;
  &:before {
    content: "";
    width: 1.2rem;
    height: 1.2rem;
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translateX(-50%) translateY(-50%);
    background-size: contain;
    background-repeat: no-repeat;
    background-position: center;
  }
  &-circle {
    border-radius: 50%;
    background-color: rgba(255, 255, 255, 0.2);
    &:before {
      width: 0.7rem;
      height: 0.7rem;
    }
    &-fraction {
      &:before {
        background-image: url("/img/chat/icon-fraction.svg");
      }
    }
    &-family {
      background-repeat: no-repeat;
      background-position: center;
      background-size: 60%;
    }
    &-call {
      &:before {
        background-image: url("/img/chat/icon-call.svg");
      }
    }
    &-message {
      &:before {
        background-image: url("/img/chat/icon-message.svg");
      }
    }

    &-tel {
      height: 0.579rem;
      width: 0.579rem;
      margin-right: 0.211rem;
      min-width: auto;
      margin-bottom: 0;
      &::before {
        width: 0.579rem;
        height: 0.579rem;
        background-image: url("/img/chat/icon-tel.svg");
      }
    }

    &-msg {
      height: 0.579rem;
      width: 0.579rem;
      margin-right: 0.211rem;
      min-width: auto;
      margin-bottom: 0;
      &::before {
        width: 0.579rem;
        height: 0.579rem;
        background-image: url("/img/chat/icon-msg.svg");
      }
    }
  }
  &-gos {
    &:before {
      background-image: url("/img/chat/icon-gos.svg");
    }
  }
  &-megaphone {
    &:before {
      background-image: url("/img/chat/icon-megaphone.svg");
    }
  }
}
.sender-wrap {
  display: flex;
  align-items: center;
  justify-content: flex-start;
  font-size: 0.75rem;
  font-weight: 300;
  padding-left: 0.3rem;
  margin-top: 0.2rem;
  margin-bottom: 0.5rem;
  .global-chat__icon-circle-call {
    background: #b6d300;
    box-shadow: 0 0 0.6rem 0 #a8c302;
  }
  .global-chat__icon-circle-message {
    margin-right: 0.5rem;
  }
  .sender__nickname {
    font-weight: 400;
    &-tittle {
      margin-right: 0.2rem;
    }
    &-phone {
      margin-left: 0.2rem;
    }
  }
}
</style>



