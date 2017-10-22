using Abide.HaloLibrary;
using System;
namespace Abide.Builder.Tags
{
    public static class AbideTags
	{
		public static Type GetTagDefinition(Tag tag)
		{
            switch (tag)
            {
                case "$#!+": return typeof(_____);
                case "adlg": return typeof(_adlg);
                case "ant!": return typeof(_ant_);
                case "bipd": return typeof(_bipd);
                case "bitm": return typeof(_bitm);
                case "bloc": return typeof(_bloc);
                case "BooM": return typeof(_BooM);
                case "bsdt": return typeof(_bsdt);
                case "char": return typeof(_char);
                case "clwd": return typeof(_clwd);
                case "coll": return typeof(_coll);
                case "coln": return typeof(_coln);
                case "colo": return typeof(_colo);
                case "cont": return typeof(_cont);
                case "crea": return typeof(_crea);
                case "ctrl": return typeof(_ctrl);
                case "deca": return typeof(_deca);
                case "DECP": return typeof(_DECP);
                case "DECR": return typeof(_DECR);
                case "devi": return typeof(_devi);
                case "devo": return typeof(_devo);
                case "dobc": return typeof(_dobc);
                case "effe": return typeof(_effe);
                case "egor": return typeof(_egor);
                case "eqip": return typeof(_eqip);
                case "fog ": return typeof(_fog_);
                case "foot": return typeof(_foot);
                case "fpch": return typeof(_fpch);
                case "garb": return typeof(_garb);
                case "gldf": return typeof(_gldf);
                case "goof": return typeof(_goof);
                case "grhi": return typeof(_grhi);
                case "hlmt": return typeof(_hlmt);
                case "hmt ": return typeof(_hmt_);
                case "hud#": return typeof(_hud_);
                case "hudg": return typeof(_hudg);
                case "item": return typeof(_item);
                case "itmc": return typeof(_itmc);
                case "jmad": return typeof(_jmad);
                case "jpt!": return typeof(_jpt_);
                case "lens": return typeof(_lens);
                case "lifi": return typeof(_lifi);
                case "ligh": return typeof(_ligh);
                case "lsnd": return typeof(_lsnd);
                case "ltmp": return typeof(_ltmp);
                case "mach": return typeof(_mach);
                case "matg": return typeof(_matg);
                case "mcsr": return typeof(_mcsr);
                case "mdlg": return typeof(_mdlg);
                case "metr": return typeof(_metr);
                case "MGS2": return typeof(_MGS2);
                case "mode": return typeof(_mode);
                case "mpdt": return typeof(_mpdt);
                case "mply": return typeof(_mply);
                case "mulg": return typeof(_mulg);
                case "nhdt": return typeof(_nhdt);
                case "obje": return typeof(_obje);
                case "phmo": return typeof(_phmo);
                case "phys": return typeof(_phys);
                case "pixl": return typeof(_pixl);
                case "pmov": return typeof(_pmov);
                case "pphy": return typeof(_pphy);
                case "proj": return typeof(_proj);
                case "prt3": return typeof(_prt3);
                case "PRTM": return typeof(_PRTM);
                case "sbsp": return typeof(_sbsp);
                case "scen": return typeof(_scen);
                case "scnr": return typeof(_scnr);
                case "sfx+": return typeof(_sfx_);
                case "shad": return typeof(_shad);
                case "sily": return typeof(_sily);
                case "skin": return typeof(_skin);
                case "sky ": return typeof(_sky_);
                case "slit": return typeof(_slit);
                case "sncl": return typeof(_sncl);
                case "snd!": return typeof(_snd_);
                case "snde": return typeof(_snde);
                case "snmx": return typeof(_snmx);
                case "spas": return typeof(_spas);
                case "spk!": return typeof(_spk_);
                case "ssce": return typeof(_ssce);
                case "sslt": return typeof(_sslt);
                case "stem": return typeof(_stem);
                case "styl": return typeof(_styl);
                case "tdtl": return typeof(_tdtl);
                case "trak": return typeof(_trak);
                case "udlg": return typeof(_udlg);
                case "ugh!": return typeof(_ugh_);
                case "unhi": return typeof(_unhi);
                case "unic": return typeof(_unic);
                case "unit": return typeof(_unit);
                case "vehc": return typeof(_vehc);
                case "vehi": return typeof(_vehi);
                case "vrtx": return typeof(_vrtx);
                case "weap": return typeof(_weap);
                case "weat": return typeof(_weat);
                case "wgit": return typeof(_wgit);
                case "wgtz": return typeof(_wgtz);
                case "whip": return typeof(_whip);
                case "wigl": return typeof(_wigl);
                case "wind": return typeof(_wind);
                case "wphi": return typeof(_wphi);
                case "<fx>": return typeof(__fx_);
                default: return null;
            }
		}
	}
}
