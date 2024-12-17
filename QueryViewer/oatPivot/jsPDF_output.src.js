
//FILE jspdf_output


/** ====================================================================
 * jsPDF IE Below 9 Shim plugin 0.1.0
 * Known to be compatible with jsPDF 0.9.0
 *
 * Copyright (c) 2013 James Hall, james@snapshotmedia.co.uk
 *
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
 * LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
 * OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
 * WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * ====================================================================
 */
if (!gx.util.browser.isIE() || 8<gx.util.browser.ieVersion()){
	
;(function(API) {
'use strict'

var name = 'jsPDF IE Below 9 Shim plugin';
// Shim in IE6-9 support
API.output = function(type, options) {

	// If not IE then return early
	return this.internal.output(type, options);

	/*var filename = 'Output.pdf';
	switch (type) {
		case 'datauristring':
		case 'dataurlstring':
		case 'datauri':
		case 'dataurl':
		case 'dataurlnewwindow':
			console.log(name + ': Data URIs are not supported on IE6-9.');
			break;
		case 'save':
			filename = options;
			break;
	}*/

	// @TODO: IE shim 


};



})(jsPDF.API)








/* FileSaver.js
 * A saveAs() FileSaver implementation.
 * 2013-01-23
 * 
 * By Eli Grey, http://eligrey.com
 * License: X11/MIT
 *   See LICENSE.md
 */

/*global self */
/*jslint bitwise: true, regexp: true, confusion: true, es5: true, vars: true, white: true,
  plusplus: true */

/*! @source http://purl.eligrey.com/github/FileSaver.js/blob/master/FileSaver.js */

var saveAs = saveAs
  || (navigator.msSaveBlob && navigator.msSaveBlob.bind(navigator))
  || (function(view) {
	"use strict";
	var
		  doc = view.document
		  // only get URL when necessary in case BlobBuilder.js hasn't overridden it yet
		, get_URL = function() {
			return view.URL || view.webkitURL || view;
		}
		, URL = view.URL || view.webkitURL || view
		, save_link = doc.createElementNS("http://www.w3.org/1999/xhtml", "a")
		, can_use_save_link = "download" in save_link
		, click = function(node) {
			var event = doc.createEvent("MouseEvents");
			event.initMouseEvent(
				"click", true, false, view, 0, 0, 0, 0, 0
				, false, false, false, false, 0, null
			);
			return node.dispatchEvent(event); // false if event was cancelled
		}
		, webkit_req_fs = view.webkitRequestFileSystem
		, req_fs = view.requestFileSystem || webkit_req_fs || view.mozRequestFileSystem
		, throw_outside = function (ex) {
			(view.setImmediate || view.setTimeout)(function() {
				throw ex;
			}, 0);
		}
		, force_saveable_type = "application/octet-stream"
		, fs_min_size = 0
		, deletion_queue = []
		, process_deletion_queue = function() {
			var i = deletion_queue.length;
			while (i--) {
				var file = deletion_queue[i];
				if (typeof file === "string") { // file is an object URL
					URL.revokeObjectURL(file);
				} else { // file is a File
					file.remove();
				}
			}
			deletion_queue.length = 0; // clear queue
		}
		, dispatch = function(filesaver, event_types, event) {
			event_types = [].concat(event_types);
			var i = event_types.length;
			while (i--) {
				var listener = filesaver["on" + event_types[i]];
				if (typeof listener === "function") {
					try {
						listener.call(filesaver, event || filesaver);
					} catch (ex) {
						throw_outside(ex);
					}
				}
			}
		}
		, FileSaver = function(blob, name) {
			// First try a.download, then web filesystem, then object URLs
			var
				  filesaver = this
				, type = blob.type
				, blob_changed = false
				, object_url
				, target_view
				, get_object_url = function() {
					var object_url = get_URL().createObjectURL(blob);
					deletion_queue.push(object_url);
					return object_url;
				}
				, dispatch_all = function() {
					dispatch(filesaver, "writestart progress write writeend".split(" "));
				}
				// on any filesys errors revert to saving with object URLs
				, fs_error = function() {
					// don't create more object URLs than needed
					if (blob_changed || !object_url) {
						object_url = get_object_url(blob);
					}
					if (target_view) {
						target_view.location.href = object_url;
					}
					filesaver.readyState = filesaver.DONE;
					dispatch_all();
				}
				, abortable = function(func) {
					return function() {
						if (filesaver.readyState !== filesaver.DONE) {
							return func.apply(this, arguments);
						}
					};
				}
				, create_if_not_found = {create: true, exclusive: false}
				, slice
			;
			filesaver.readyState = filesaver.INIT;
			if (!name) {
				name = "download";
			}
			if (can_use_save_link) {
				object_url = get_object_url(blob);
				save_link.href = object_url;
				save_link.download = name;
				if (click(save_link)) {
					filesaver.readyState = filesaver.DONE;
					dispatch_all();
					return;
				}
			}
			// Object and web filesystem URLs have a problem saving in Google Chrome when
			// viewed in a tab, so I force save with application/octet-stream
			// http://code.google.com/p/chromium/issues/detail?id=91158
			if (view.chrome && type && type !== force_saveable_type) {
				slice = blob.slice || blob.webkitSlice;
				blob = slice.call(blob, 0, blob.size, force_saveable_type);
				blob_changed = true;
			}
			// Since I can't be sure that the guessed media type will trigger a download
			// in WebKit, I append .download to the filename.
			// https://bugs.webkit.org/show_bug.cgi?id=65440
			if (webkit_req_fs && name !== "download") {
				name += ".download";
			}
			if (type === force_saveable_type || webkit_req_fs) {
				target_view = view;
			} else {
			//	target_view = view.open();
			}
			if (!req_fs) {
				fs_error();
				return;
			}
			fs_min_size += blob.size;
			req_fs(view.TEMPORARY, fs_min_size, abortable(function(fs) {
				fs.root.getDirectory("saved", create_if_not_found, abortable(function(dir) {
					var save = function() {
						dir.getFile(name, create_if_not_found, abortable(function(file) {
							file.createWriter(abortable(function(writer) {
								writer.onwriteend = function(event) {
									target_view.location.href = file.toURL();
									deletion_queue.push(file);
									filesaver.readyState = filesaver.DONE;
									dispatch(filesaver, "writeend", event);
								};
								writer.onerror = function() {
									var error = writer.error;
									if (error.code !== error.ABORT_ERR) {
										fs_error();
									}
								};
								"writestart progress write abort".split(" ").forEach(function(event) {
									writer["on" + event] = filesaver["on" + event];
								});
								writer.write(blob);
								filesaver.abort = function() {
									writer.abort();
									filesaver.readyState = filesaver.DONE;
								};
								filesaver.readyState = filesaver.WRITING;
							}), fs_error);
						}), fs_error);
					};
					dir.getFile(name, {create: false}, abortable(function(file) {
						// delete file if it already exists
						file.remove();
						save();
					}), abortable(function(ex) {
						if (ex.code === ex.NOT_FOUND_ERR) {
							save();
						} else {
							fs_error();
						}
					}));
				}), fs_error);
			}), fs_error);
		}
		, FS_proto = FileSaver.prototype
		, saveAs = function(blob, name) {
			return new FileSaver(blob, name);
		}
	;
	FS_proto.abort = function() {
		var filesaver = this;
		filesaver.readyState = filesaver.DONE;
		dispatch(filesaver, "abort");
	};
	FS_proto.readyState = FS_proto.INIT = 0;
	FS_proto.WRITING = 1;
	FS_proto.DONE = 2;
	
	FS_proto.error =
	FS_proto.onwritestart =
	FS_proto.onprogress =
	FS_proto.onwrite =
	FS_proto.onabort =
	FS_proto.onerror =
	FS_proto.onwriteend =
		null;
	
	view.addEventListener("unload", process_deletion_queue, false);
	return saveAs;
}(self));



/*
 Copyright (c) 2013 Gildas Lormeau. All rights reserved.

 Redistribution and use in source and binary forms, with or without
 modification, are permitted provided that the following conditions are met:

 1. Redistributions of source code must retain the above copyright notice,
 this list of conditions and the following disclaimer.

 2. Redistributions in binary form must reproduce the above copyright 
 notice, this list of conditions and the following disclaimer in 
 the documentation and/or other materials provided with the distribution.

 3. The names of the authors may not be used to endorse or promote products
 derived from this software without specific prior written permission.

 THIS SOFTWARE IS PROVIDED ``AS IS'' AND ANY EXPRESSED OR IMPLIED WARRANTIES,
 INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
 FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL JCRAFT,
 INC. OR ANY CONTRIBUTORS TO THIS SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT,
 INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA,
 OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
 LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
 EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

/*
 * This program is based on JZlib 1.0.2 ymnk, JCraft,Inc.
 * JZlib is based on zlib-1.1.3, so all credit should go authors
 * Jean-loup Gailly(jloup@gzip.org) and Mark Adler(madler@alumni.caltech.edu)
 * and contributors of zlib.
 */

// Global

var MAX_BITS = 15;
var D_CODES = 30;
var BL_CODES = 19;

var LENGTH_CODES = 29;
var LITERALS = 256;
var L_CODES = (LITERALS + 1 + LENGTH_CODES);
var HEAP_SIZE = (2 * L_CODES + 1);

var END_BLOCK = 256;

// Bit length codes must not exceed MAX_BL_BITS bits
var MAX_BL_BITS = 7;

// repeat previous bit length 3-6 times (2 bits of repeat count)
var REP_3_6 = 16;

// repeat a zero length 3-10 times (3 bits of repeat count)
var REPZ_3_10 = 17;

// repeat a zero length 11-138 times (7 bits of repeat count)
var REPZ_11_138 = 18;

// The lengths of the bit length codes are sent in order of decreasing
// probability, to avoid transmitting the lengths for unused bit
// length codes.

var Buf_size = 8 * 2;

// JZlib version : "1.0.2"
var Z_DEFAULT_COMPRESSION = -1;

// compression strategy
var Z_FILTERED = 1;
var Z_HUFFMAN_ONLY = 2;
var Z_DEFAULT_STRATEGY = 0;

var Z_NO_FLUSH = 0;
var Z_PARTIAL_FLUSH = 1;
var Z_FULL_FLUSH = 3;
var Z_FINISH = 4;

var Z_OK = 0;
var Z_STREAM_END = 1;
var Z_NEED_DICT = 2;
var Z_STREAM_ERROR = -2;
var Z_DATA_ERROR = -3;
var Z_BUF_ERROR = -5;

// Tree

// see definition of array dist_code below
var _dist_code = [ 0, 1, 2, 3, 4, 4, 5, 5, 6, 6, 6, 6, 7, 7, 7, 7, 8, 8, 8, 8, 8, 8, 8, 8, 9, 9, 9, 9, 9, 9, 9, 9, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
        10, 10, 10, 10, 10, 10, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12,
        12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13,
        13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 13, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
        14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
        14, 14, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15,
        15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 0, 0, 16, 17, 18, 18, 19, 19,
        20, 20, 20, 20, 21, 21, 21, 21, 22, 22, 22, 22, 22, 22, 22, 22, 23, 23, 23, 23, 23, 23, 23, 23, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
        24, 24, 24, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
        26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
        27, 27, 27, 27, 27, 27, 27, 27, 27, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28,
        28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 28, 29,
        29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29,
        29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29 ];

function Tree() {
    'use strict';
    var that = this;

    // dyn_tree; // the dynamic tree
    // max_code; // largest code with non zero frequency
    // stat_desc; // the corresponding static tree

    // Compute the optimal bit lengths for a tree and update the total bit
    // length
    // for the current block.
    // IN assertion: the fields freq and dad are set, heap[heap_max] and
    // above are the tree nodes sorted by increasing frequency.
    // OUT assertions: the field len is set to the optimal bit length, the
    // array bl_count contains the frequencies for each bit length.
    // The length opt_len is updated; static_len is also updated if stree is
    // not null.
    function gen_bitlen(s) {
        var tree = that.dyn_tree;
        var stree = that.stat_desc.static_tree;
        var extra = that.stat_desc.extra_bits;
        var base = that.stat_desc.extra_base;
        var max_length = that.stat_desc.max_length;
        var h; // heap index
        var n, m; // iterate over the tree elements
        var bits; // bit length
        var xbits; // extra bits
        var f; // frequency
        var overflow = 0; // number of elements with bit length too large

        for (bits = 0; bits <= MAX_BITS; bits++)
            s.bl_count[bits] = 0;

        // In a first pass, compute the optimal bit lengths (which may
        // overflow in the case of the bit length tree).
        tree[s.heap[s.heap_max] * 2 + 1] = 0; // root of the heap

        for (h = s.heap_max + 1; h < HEAP_SIZE; h++) {
            n = s.heap[h];
            bits = tree[tree[n * 2 + 1] * 2 + 1] + 1;
            if (bits > max_length) {
                bits = max_length;
                overflow++;
            }
            tree[n * 2 + 1] = bits;
            // We overwrite tree[n*2+1] which is no longer needed

            if (n > that.max_code)
                continue; // not a leaf node

            s.bl_count[bits]++;
            xbits = 0;
            if (n >= base)
                xbits = extra[n - base];
            f = tree[n * 2];
            s.opt_len += f * (bits + xbits);
            if (stree)
                s.static_len += f * (stree[n * 2 + 1] + xbits);
        }
        if (overflow === 0)
            return;

        // This happens for example on obj2 and pic of the Calgary corpus
        // Find the first bit length which could increase:
        do {
            bits = max_length - 1;
            while (s.bl_count[bits] === 0)
                bits--;
            s.bl_count[bits]--; // move one leaf down the tree
            s.bl_count[bits + 1] += 2; // move one overflow item as its brother
            s.bl_count[max_length]--;
            // The brother of the overflow item also moves one step up,
            // but this does not affect bl_count[max_length]
            overflow -= 2;
        } while (overflow > 0);

        for (bits = max_length; bits !== 0; bits--) {
            n = s.bl_count[bits];
            while (n !== 0) {
                m = s.heap[--h];
                if (m > that.max_code)
                    continue;
                if (tree[m * 2 + 1] != bits) {
                    s.opt_len += (bits - tree[m * 2 + 1]) * tree[m * 2];
                    tree[m * 2 + 1] = bits;
                }
                n--;
            }
        }
    }

    // Reverse the first len bits of a code, using straightforward code (a
    // faster
    // method would use a table)
    // IN assertion: 1 <= len <= 15
    function bi_reverse(code, // the value to invert
    len // its bit length
    ) {
        var res = 0;
        do {
            res |= code & 1;
            code >>>= 1;
            res <<= 1;
        } while (--len > 0);
        return res >>> 1;
    }

    // Generate the codes for a given tree and bit counts (which need not be
    // optimal).
    // IN assertion: the array bl_count contains the bit length statistics for
    // the given tree and the field len is set for all tree elements.
    // OUT assertion: the field code is set for all tree elements of non
    // zero code length.
    function gen_codes(tree, // the tree to decorate
    max_code, // largest code with non zero frequency
    bl_count // number of codes at each bit length
    ) {
        var next_code = []; // next code value for each
        // bit length
        var code = 0; // running code value
        var bits; // bit index
        var n; // code index
        var len;

        // The distribution counts are first used to generate the code values
        // without bit reversal.
        for (bits = 1; bits <= MAX_BITS; bits++) {
            next_code[bits] = code = ((code + bl_count[bits - 1]) << 1);
        }

        // Check that the bit counts in bl_count are consistent. The last code
        // must be all ones.
        // Assert (code + bl_count[MAX_BITS]-1 == (1<<MAX_BITS)-1,
        // "inconsistent bit counts");
        // Tracev((stderr,"\ngen_codes: max_code %d ", max_code));

        for (n = 0; n <= max_code; n++) {
            len = tree[n * 2 + 1];
            if (len === 0)
                continue;
            // Now reverse the bits
            tree[n * 2] = bi_reverse(next_code[len]++, len);
        }
    }

    // Construct one Huffman tree and assigns the code bit strings and lengths.
    // Update the total bit length for the current block.
    // IN assertion: the field freq is set for all tree elements.
    // OUT assertions: the fields len and code are set to the optimal bit length
    // and corresponding code. The length opt_len is updated; static_len is
    // also updated if stree is not null. The field max_code is set.
    that.build_tree = function(s) {
        var tree = that.dyn_tree;
        var stree = that.stat_desc.static_tree;
        var elems = that.stat_desc.elems;
        var n, m; // iterate over heap elements
        var max_code = -1; // largest code with non zero frequency
        var node; // new node being created

        // Construct the initial heap, with least frequent element in
        // heap[1]. The sons of heap[n] are heap[2*n] and heap[2*n+1].
        // heap[0] is not used.
        s.heap_len = 0;
        s.heap_max = HEAP_SIZE;

        for (n = 0; n < elems; n++) {
            if (tree[n * 2] !== 0) {
                s.heap[++s.heap_len] = max_code = n;
                s.depth[n] = 0;
            } else {
                tree[n * 2 + 1] = 0;
            }
        }

        // The pkzip format requires that at least one distance code exists,
        // and that at least one bit should be sent even if there is only one
        // possible code. So to avoid special checks later on we force at least
        // two codes of non zero frequency.
        while (s.heap_len < 2) {
            node = s.heap[++s.heap_len] = max_code < 2 ? ++max_code : 0;
            tree[node * 2] = 1;
            s.depth[node] = 0;
            s.opt_len--;
            if (stree)
                s.static_len -= stree[node * 2 + 1];
            // node is 0 or 1 so it does not have extra bits
        }
        that.max_code = max_code;

        // The elements heap[heap_len/2+1 .. heap_len] are leaves of the tree,
        // establish sub-heaps of increasing lengths:

        for (n = Math.floor(s.heap_len / 2); n >= 1; n--)
            s.pqdownheap(tree, n);

        // Construct the Huffman tree by repeatedly combining the least two
        // frequent nodes.

        node = elems; // next internal node of the tree
        do {
            // n = node of least frequency
            n = s.heap[1];
            s.heap[1] = s.heap[s.heap_len--];
            s.pqdownheap(tree, 1);
            m = s.heap[1]; // m = node of next least frequency

            s.heap[--s.heap_max] = n; // keep the nodes sorted by frequency
            s.heap[--s.heap_max] = m;

            // Create a new node father of n and m
            tree[node * 2] = (tree[n * 2] + tree[m * 2]);
            s.depth[node] = Math.max(s.depth[n], s.depth[m]) + 1;
            tree[n * 2 + 1] = tree[m * 2 + 1] = node;

            // and insert the new node in the heap
            s.heap[1] = node++;
            s.pqdownheap(tree, 1);
        } while (s.heap_len >= 2);

        s.heap[--s.heap_max] = s.heap[1];

        // At this point, the fields freq and dad are set. We can now
        // generate the bit lengths.

        gen_bitlen(s);

        // The field len is now set, we can generate the bit codes
        gen_codes(tree, that.max_code, s.bl_count);
    };

}

Tree._length_code = [ 0, 1, 2, 3, 4, 5, 6, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 12, 12, 13, 13, 13, 13, 14, 14, 14, 14, 15, 15, 15, 15, 16, 16, 16, 16,
        16, 16, 16, 16, 17, 17, 17, 17, 17, 17, 17, 17, 18, 18, 18, 18, 18, 18, 18, 18, 19, 19, 19, 19, 19, 19, 19, 19, 20, 20, 20, 20, 20, 20, 20, 20, 20,
        20, 20, 20, 20, 20, 20, 20, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
        22, 22, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 23, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24,
        24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
        25, 25, 25, 25, 25, 25, 25, 25, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26, 26,
        26, 26, 26, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 27, 28 ];

Tree.base_length = [ 0, 1, 2, 3, 4, 5, 6, 7, 8, 10, 12, 14, 16, 20, 24, 28, 32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 0 ];

Tree.base_dist = [ 0, 1, 2, 3, 4, 6, 8, 12, 16, 24, 32, 48, 64, 96, 128, 192, 256, 384, 512, 768, 1024, 1536, 2048, 3072, 4096, 6144, 8192, 12288, 16384,
        24576 ];

// Mapping from a distance to a distance code. dist is the distance - 1 and
// must not have side effects. _dist_code[256] and _dist_code[257] are never
// used.
Tree.d_code = function(dist) {
    return ((dist) < 256 ? _dist_code[dist] : _dist_code[256 + ((dist) >>> 7)]);
};

// extra bits for each length code
Tree.extra_lbits = [ 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 0 ];

// extra bits for each distance code
Tree.extra_dbits = [ 0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13 ];

// extra bits for each bit length code
Tree.extra_blbits = [ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, 7 ];

Tree.bl_order = [ 16, 17, 18, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 14, 1, 15 ];

// StaticTree

function StaticTree(static_tree, extra_bits, extra_base, elems, max_length) {
    var that = this;
    that.static_tree = static_tree;
    that.extra_bits = extra_bits;
    that.extra_base = extra_base;
    that.elems = elems;
    that.max_length = max_length;
}

StaticTree.static_ltree = [ 12, 8, 140, 8, 76, 8, 204, 8, 44, 8, 172, 8, 108, 8, 236, 8, 28, 8, 156, 8, 92, 8, 220, 8, 60, 8, 188, 8, 124, 8, 252, 8, 2, 8,
        130, 8, 66, 8, 194, 8, 34, 8, 162, 8, 98, 8, 226, 8, 18, 8, 146, 8, 82, 8, 210, 8, 50, 8, 178, 8, 114, 8, 242, 8, 10, 8, 138, 8, 74, 8, 202, 8, 42,
        8, 170, 8, 106, 8, 234, 8, 26, 8, 154, 8, 90, 8, 218, 8, 58, 8, 186, 8, 122, 8, 250, 8, 6, 8, 134, 8, 70, 8, 198, 8, 38, 8, 166, 8, 102, 8, 230, 8,
        22, 8, 150, 8, 86, 8, 214, 8, 54, 8, 182, 8, 118, 8, 246, 8, 14, 8, 142, 8, 78, 8, 206, 8, 46, 8, 174, 8, 110, 8, 238, 8, 30, 8, 158, 8, 94, 8,
        222, 8, 62, 8, 190, 8, 126, 8, 254, 8, 1, 8, 129, 8, 65, 8, 193, 8, 33, 8, 161, 8, 97, 8, 225, 8, 17, 8, 145, 8, 81, 8, 209, 8, 49, 8, 177, 8, 113,
        8, 241, 8, 9, 8, 137, 8, 73, 8, 201, 8, 41, 8, 169, 8, 105, 8, 233, 8, 25, 8, 153, 8, 89, 8, 217, 8, 57, 8, 185, 8, 121, 8, 249, 8, 5, 8, 133, 8,
        69, 8, 197, 8, 37, 8, 165, 8, 101, 8, 229, 8, 21, 8, 149, 8, 85, 8, 213, 8, 53, 8, 181, 8, 117, 8, 245, 8, 13, 8, 141, 8, 77, 8, 205, 8, 45, 8,
        173, 8, 109, 8, 237, 8, 29, 8, 157, 8, 93, 8, 221, 8, 61, 8, 189, 8, 125, 8, 253, 8, 19, 9, 275, 9, 147, 9, 403, 9, 83, 9, 339, 9, 211, 9, 467, 9,
        51, 9, 307, 9, 179, 9, 435, 9, 115, 9, 371, 9, 243, 9, 499, 9, 11, 9, 267, 9, 139, 9, 395, 9, 75, 9, 331, 9, 203, 9, 459, 9, 43, 9, 299, 9, 171, 9,
        427, 9, 107, 9, 363, 9, 235, 9, 491, 9, 27, 9, 283, 9, 155, 9, 411, 9, 91, 9, 347, 9, 219, 9, 475, 9, 59, 9, 315, 9, 187, 9, 443, 9, 123, 9, 379,
        9, 251, 9, 507, 9, 7, 9, 263, 9, 135, 9, 391, 9, 71, 9, 327, 9, 199, 9, 455, 9, 39, 9, 295, 9, 167, 9, 423, 9, 103, 9, 359, 9, 231, 9, 487, 9, 23,
        9, 279, 9, 151, 9, 407, 9, 87, 9, 343, 9, 215, 9, 471, 9, 55, 9, 311, 9, 183, 9, 439, 9, 119, 9, 375, 9, 247, 9, 503, 9, 15, 9, 271, 9, 143, 9,
        399, 9, 79, 9, 335, 9, 207, 9, 463, 9, 47, 9, 303, 9, 175, 9, 431, 9, 111, 9, 367, 9, 239, 9, 495, 9, 31, 9, 287, 9, 159, 9, 415, 9, 95, 9, 351, 9,
        223, 9, 479, 9, 63, 9, 319, 9, 191, 9, 447, 9, 127, 9, 383, 9, 255, 9, 511, 9, 0, 7, 64, 7, 32, 7, 96, 7, 16, 7, 80, 7, 48, 7, 112, 7, 8, 7, 72, 7,
        40, 7, 104, 7, 24, 7, 88, 7, 56, 7, 120, 7, 4, 7, 68, 7, 36, 7, 100, 7, 20, 7, 84, 7, 52, 7, 116, 7, 3, 8, 131, 8, 67, 8, 195, 8, 35, 8, 163, 8,
        99, 8, 227, 8 ];

StaticTree.static_dtree = [ 0, 5, 16, 5, 8, 5, 24, 5, 4, 5, 20, 5, 12, 5, 28, 5, 2, 5, 18, 5, 10, 5, 26, 5, 6, 5, 22, 5, 14, 5, 30, 5, 1, 5, 17, 5, 9, 5,
        25, 5, 5, 5, 21, 5, 13, 5, 29, 5, 3, 5, 19, 5, 11, 5, 27, 5, 7, 5, 23, 5 ];

StaticTree.static_l_desc = new StaticTree(StaticTree.static_ltree, Tree.extra_lbits, LITERALS + 1, L_CODES, MAX_BITS);

StaticTree.static_d_desc = new StaticTree(StaticTree.static_dtree, Tree.extra_dbits, 0, D_CODES, MAX_BITS);

StaticTree.static_bl_desc = new StaticTree(null, Tree.extra_blbits, 0, BL_CODES, MAX_BL_BITS);

// Deflate

var MAX_MEM_LEVEL = 9;
var DEF_MEM_LEVEL = 8;

function Config(good_length, max_lazy, nice_length, max_chain, func) {
    var that = this;
    that.good_length = good_length;
    that.max_lazy = max_lazy;
    that.nice_length = nice_length;
    that.max_chain = max_chain;
    that.func = func;
}

var STORED = 0;
var FAST = 1;
var SLOW = 2;
var config_table = [ new Config(0, 0, 0, 0, STORED), new Config(4, 4, 8, 4, FAST), new Config(4, 5, 16, 8, FAST), new Config(4, 6, 32, 32, FAST),
        new Config(4, 4, 16, 16, SLOW), new Config(8, 16, 32, 32, SLOW), new Config(8, 16, 128, 128, SLOW), new Config(8, 32, 128, 256, SLOW),
        new Config(32, 128, 258, 1024, SLOW), new Config(32, 258, 258, 4096, SLOW) ];

var z_errmsg = [ "need dictionary", // Z_NEED_DICT
// 2
"stream end", // Z_STREAM_END 1
"", // Z_OK 0
"", // Z_ERRNO (-1)
"stream error", // Z_STREAM_ERROR (-2)
"data error", // Z_DATA_ERROR (-3)
"", // Z_MEM_ERROR (-4)
"buffer error", // Z_BUF_ERROR (-5)
"",// Z_VERSION_ERROR (-6)
"" ];

// block not completed, need more input or more output
var NeedMore = 0;

// block flush performed
var BlockDone = 1;

// finish started, need only more output at next deflate
var FinishStarted = 2;

// finish done, accept no more input or output
var FinishDone = 3;

// preset dictionary flag in zlib header
var PRESET_DICT = 0x20;

var INIT_STATE = 42;
var BUSY_STATE = 113;
var FINISH_STATE = 666;

// The deflate compression method
var Z_DEFLATED = 8;

var STORED_BLOCK = 0;
var STATIC_TREES = 1;
var DYN_TREES = 2;

var MIN_MATCH = 3;
var MAX_MATCH = 258;
var MIN_LOOKAHEAD = (MAX_MATCH + MIN_MATCH + 1);

function smaller(tree, n, m, depth) {
    var tn2 = tree[n * 2];
    var tm2 = tree[m * 2];
    return (tn2 < tm2 || (tn2 == tm2 && depth[n] <= depth[m]));
}

function Deflate() {

    var that = this;
    var strm; // pointer back to this zlib stream
    var status; // as the name implies
    // pending_buf; // output still pending
    var pending_buf_size; // size of pending_buf
    // pending_out; // next pending byte to output to the stream
    // pending; // nb of bytes in the pending buffer
    var method; // STORED (for zip only) or DEFLATED
    var last_flush; // value of flush param for previous deflate call

    var w_size; // LZ77 window size (32K by default)
    var w_bits; // log2(w_size) (8..16)
    var w_mask; // w_size - 1

    var window;
    // Sliding window. Input bytes are read into the second half of the window,
    // and move to the first half later to keep a dictionary of at least wSize
    // bytes. With this organization, matches are limited to a distance of
    // wSize-MAX_MATCH bytes, but this ensures that IO is always
    // performed with a length multiple of the block size. Also, it limits
    // the window size to 64K, which is quite useful on MSDOS.
    // To do: use the user input buffer as sliding window.

    var window_size;
    // Actual size of window: 2*wSize, except when the user input buffer
    // is directly used as sliding window.

    var prev;
    // Link to older string with same hash index. To limit the size of this
    // array to 64K, this link is maintained only for the last 32K strings.
    // An index in this array is thus a window index modulo 32K.

    var head; // Heads of the hash chains or NIL.

    var ins_h; // hash index of string to be inserted
    var hash_size; // number of elements in hash table
    var hash_bits; // log2(hash_size)
    var hash_mask; // hash_size-1

    // Number of bits by which ins_h must be shifted at each input
    // step. It must be such that after MIN_MATCH steps, the oldest
    // byte no longer takes part in the hash key, that is:
    // hash_shift * MIN_MATCH >= hash_bits
    var hash_shift;

    // Window position at the beginning of the current output block. Gets
    // negative when the window is moved backwards.

    var block_start;

    var match_length; // length of best match
    var prev_match; // previous match
    var match_available; // set if previous match exists
    var strstart; // start of string to insert
    var match_start; // start of matching string
    var lookahead; // number of valid bytes ahead in window

    // Length of the best match at previous step. Matches not greater than this
    // are discarded. This is used in the lazy match evaluation.
    var prev_length;

    // To speed up deflation, hash chains are never searched beyond this
    // length. A higher limit improves compression ratio but degrades the speed.
    var max_chain_length;

    // Attempt to find a better match only when the current match is strictly
    // smaller than this value. This mechanism is used only for compression
    // levels >= 4.
    var max_lazy_match;

    // Insert new strings in the hash table only if the match length is not
    // greater than this length. This saves time but degrades compression.
    // max_insert_length is used only for compression levels <= 3.

    var level; // compression level (1..9)
    var strategy; // favor or force Huffman coding

    // Use a faster search when the previous match is longer than this
    var good_match;

    // Stop searching when current match exceeds this
    var nice_match;

    var dyn_ltree; // literal and length tree
    var dyn_dtree; // distance tree
    var bl_tree; // Huffman tree for bit lengths

    var l_desc = new Tree(); // desc for literal tree
    var d_desc = new Tree(); // desc for distance tree
    var bl_desc = new Tree(); // desc for bit length tree

    // that.heap_len; // number of elements in the heap
    // that.heap_max; // element of largest frequency
    // The sons of heap[n] are heap[2*n] and heap[2*n+1]. heap[0] is not used.
    // The same heap array is used to build all trees.

    // Depth of each subtree used as tie breaker for trees of equal frequency
    that.depth = [];

    var l_buf; // index for literals or lengths */

    // Size of match buffer for literals/lengths. There are 4 reasons for
    // limiting lit_bufsize to 64K:
    // - frequencies can be kept in 16 bit counters
    // - if compression is not successful for the first block, all input
    // data is still in the window so we can still emit a stored block even
    // when input comes from standard input. (This can also be done for
    // all blocks if lit_bufsize is not greater than 32K.)
    // - if compression is not successful for a file smaller than 64K, we can
    // even emit a stored file instead of a stored block (saving 5 bytes).
    // This is applicable only for zip (not gzip or zlib).
    // - creating new Huffman trees less frequently may not provide fast
    // adaptation to changes in the input data statistics. (Take for
    // example a binary file with poorly compressible code followed by
    // a highly compressible string table.) Smaller buffer sizes give
    // fast adaptation but have of course the overhead of transmitting
    // trees more frequently.
    // - I can't count above 4
    var lit_bufsize;

    var last_lit; // running index in l_buf

    // Buffer for distances. To simplify the code, d_buf and l_buf have
    // the same number of elements. To use different lengths, an extra flag
    // array would be necessary.

    var d_buf; // index of pendig_buf

    // that.opt_len; // bit length of current block with optimal trees
    // that.static_len; // bit length of current block with static trees
    var matches; // number of string matches in current block
    var last_eob_len; // bit length of EOB code for last block

    // Output buffer. bits are inserted starting at the bottom (least
    // significant bits).
    var bi_buf;

    // Number of valid bits in bi_buf. All bits above the last valid bit
    // are always zero.
    var bi_valid;

    // number of codes at each bit length for an optimal tree
    that.bl_count = [];

    // heap used to build the Huffman trees
    that.heap = [];

    dyn_ltree = [];
    dyn_dtree = [];
    bl_tree = [];

    function lm_init() {
        var i;
        window_size = 2 * w_size;

        head[hash_size - 1] = 0;
        for (i = 0; i < hash_size - 1; i++) {
            head[i] = 0;
        }

        // Set the default configuration parameters:
        max_lazy_match = config_table[level].max_lazy;
        good_match = config_table[level].good_length;
        nice_match = config_table[level].nice_length;
        max_chain_length = config_table[level].max_chain;

        strstart = 0;
        block_start = 0;
        lookahead = 0;
        match_length = prev_length = MIN_MATCH - 1;
        match_available = 0;
        ins_h = 0;
    }

    function init_block() {
        var i;
        // Initialize the trees.
        for (i = 0; i < L_CODES; i++)
            dyn_ltree[i * 2] = 0;
        for (i = 0; i < D_CODES; i++)
            dyn_dtree[i * 2] = 0;
        for (i = 0; i < BL_CODES; i++)
            bl_tree[i * 2] = 0;

        dyn_ltree[END_BLOCK * 2] = 1;
        that.opt_len = that.static_len = 0;
        last_lit = matches = 0;
    }

    // Initialize the tree data structures for a new zlib stream.
    function tr_init() {

        l_desc.dyn_tree = dyn_ltree;
        l_desc.stat_desc = StaticTree.static_l_desc;

        d_desc.dyn_tree = dyn_dtree;
        d_desc.stat_desc = StaticTree.static_d_desc;

        bl_desc.dyn_tree = bl_tree;
        bl_desc.stat_desc = StaticTree.static_bl_desc;

        bi_buf = 0;
        bi_valid = 0;
        last_eob_len = 8; // enough lookahead for inflate

        // Initialize the first block of the first file:
        init_block();
    }

    // Restore the heap property by moving down the tree starting at node k,
    // exchanging a node with the smallest of its two sons if necessary,
    // stopping
    // when the heap property is re-established (each father smaller than its
    // two sons).
    that.pqdownheap = function(tree, // the tree to restore
    k // node to move down
    ) {
        var heap = that.heap;
        var v = heap[k];
        var j = k << 1; // left son of k
        while (j <= that.heap_len) {
            // Set j to the smallest of the two sons:
            if (j < that.heap_len && smaller(tree, heap[j + 1], heap[j], that.depth)) {
                j++;
            }
            // Exit if v is smaller than both sons
            if (smaller(tree, v, heap[j], that.depth))
                break;

            // Exchange v with the smallest son
            heap[k] = heap[j];
            k = j;
            // And continue down the tree, setting j to the left son of k
            j <<= 1;
        }
        heap[k] = v;
    };

    // Scan a literal or distance tree to determine the frequencies of the codes
    // in the bit length tree.
    function scan_tree(tree,// the tree to be scanned
    max_code // and its largest code of non zero frequency
    ) {
        var n; // iterates over all tree elements
        var prevlen = -1; // last emitted length
        var curlen; // length of current code
        var nextlen = tree[0 * 2 + 1]; // length of next code
        var count = 0; // repeat count of the current code
        var max_count = 7; // max repeat count
        var min_count = 4; // min repeat count

        if (nextlen === 0) {
            max_count = 138;
            min_count = 3;
        }
        tree[(max_code + 1) * 2 + 1] = 0xffff; // guard

        for (n = 0; n <= max_code; n++) {
            curlen = nextlen;
            nextlen = tree[(n + 1) * 2 + 1];
            if (++count < max_count && curlen == nextlen) {
                continue;
            } else if (count < min_count) {
                bl_tree[curlen * 2] += count;
            } else if (curlen !== 0) {
                if (curlen != prevlen)
                    bl_tree[curlen * 2]++;
                bl_tree[REP_3_6 * 2]++;
            } else if (count <= 10) {
                bl_tree[REPZ_3_10 * 2]++;
            } else {
                bl_tree[REPZ_11_138 * 2]++;
            }
            count = 0;
            prevlen = curlen;
            if (nextlen === 0) {
                max_count = 138;
                min_count = 3;
            } else if (curlen == nextlen) {
                max_count = 6;
                min_count = 3;
            } else {
                max_count = 7;
                min_count = 4;
            }
        }
    }

    // Construct the Huffman tree for the bit lengths and return the index in
    // bl_order of the last bit length code to send.
    function build_bl_tree() {
        var max_blindex; // index of last bit length code of non zero freq

        // Determine the bit length frequencies for literal and distance trees
        scan_tree(dyn_ltree, l_desc.max_code);
        scan_tree(dyn_dtree, d_desc.max_code);

        // Build the bit length tree:
        bl_desc.build_tree(that);
        // opt_len now includes the length of the tree representations, except
        // the lengths of the bit lengths codes and the 5+5+4 bits for the
        // counts.

        // Determine the number of bit length codes to send. The pkzip format
        // requires that at least 4 bit length codes be sent. (appnote.txt says
        // 3 but the actual value used is 4.)
        for (max_blindex = BL_CODES - 1; max_blindex >= 3; max_blindex--) {
            if (bl_tree[Tree.bl_order[max_blindex] * 2 + 1] !== 0)
                break;
        }
        // Update opt_len to include the bit length tree and counts
        that.opt_len += 3 * (max_blindex + 1) + 5 + 5 + 4;

        return max_blindex;
    }

    // Output a byte on the stream.
    // IN assertion: there is enough room in pending_buf.
    function put_byte(p) {
        that.pending_buf[that.pending++] = p;
    }

    function put_short(w) {
        put_byte(w & 0xff);
        put_byte((w >>> 8) & 0xff);
    }

    function putShortMSB(b) {
        put_byte((b >> 8) & 0xff);
        put_byte((b & 0xff) & 0xff);
    }

    function send_bits(value, length) {
        var val, len = length;
        if (bi_valid > Buf_size - len) {
            val = value;
            // bi_buf |= (val << bi_valid);
            bi_buf |= ((val << bi_valid) & 0xffff);
            put_short(bi_buf);
            bi_buf = val >>> (Buf_size - bi_valid);
            bi_valid += len - Buf_size;
        } else {
            // bi_buf |= (value) << bi_valid;
            bi_buf |= (((value) << bi_valid) & 0xffff);
            bi_valid += len;
        }
    }

    function send_code(c, tree) {
        var c2 = c * 2;
        send_bits(tree[c2] & 0xffff, tree[c2 + 1] & 0xffff);
    }

    // Send a literal or distance tree in compressed form, using the codes in
    // bl_tree.
    function send_tree(tree,// the tree to be sent
    max_code // and its largest code of non zero frequency
    ) {
        var n; // iterates over all tree elements
        var prevlen = -1; // last emitted length
        var curlen; // length of current code
        var nextlen = tree[0 * 2 + 1]; // length of next code
        var count = 0; // repeat count of the current code
        var max_count = 7; // max repeat count
        var min_count = 4; // min repeat count

        if (nextlen === 0) {
            max_count = 138;
            min_count = 3;
        }

        for (n = 0; n <= max_code; n++) {
            curlen = nextlen;
            nextlen = tree[(n + 1) * 2 + 1];
            if (++count < max_count && curlen == nextlen) {
                continue;
            } else if (count < min_count) {
                do {
                    send_code(curlen, bl_tree);
                } while (--count !== 0);
            } else if (curlen !== 0) {
                if (curlen != prevlen) {
                    send_code(curlen, bl_tree);
                    count--;
                }
                send_code(REP_3_6, bl_tree);
                send_bits(count - 3, 2);
            } else if (count <= 10) {
                send_code(REPZ_3_10, bl_tree);
                send_bits(count - 3, 3);
            } else {
                send_code(REPZ_11_138, bl_tree);
                send_bits(count - 11, 7);
            }
            count = 0;
            prevlen = curlen;
            if (nextlen === 0) {
                max_count = 138;
                min_count = 3;
            } else if (curlen == nextlen) {
                max_count = 6;
                min_count = 3;
            } else {
                max_count = 7;
                min_count = 4;
            }
        }
    }

    // Send the header for a block using dynamic Huffman trees: the counts, the
    // lengths of the bit length codes, the literal tree and the distance tree.
    // IN assertion: lcodes >= 257, dcodes >= 1, blcodes >= 4.
    function send_all_trees(lcodes, dcodes, blcodes) {
        var rank; // index in bl_order

        send_bits(lcodes - 257, 5); // not +255 as stated in appnote.txt
        send_bits(dcodes - 1, 5);
        send_bits(blcodes - 4, 4); // not -3 as stated in appnote.txt
        for (rank = 0; rank < blcodes; rank++) {
            send_bits(bl_tree[Tree.bl_order[rank] * 2 + 1], 3);
        }
        send_tree(dyn_ltree, lcodes - 1); // literal tree
        send_tree(dyn_dtree, dcodes - 1); // distance tree
    }

    // Flush the bit buffer, keeping at most 7 bits in it.
    function bi_flush() {
        if (bi_valid == 16) {
            put_short(bi_buf);
            bi_buf = 0;
            bi_valid = 0;
        } else if (bi_valid >= 8) {
            put_byte(bi_buf & 0xff);
            bi_buf >>>= 8;
            bi_valid -= 8;
        }
    }

    // Send one empty static block to give enough lookahead for inflate.
    // This takes 10 bits, of which 7 may remain in the bit buffer.
    // The current inflate code requires 9 bits of lookahead. If the
    // last two codes for the previous block (real code plus EOB) were coded
    // on 5 bits or less, inflate may have only 5+3 bits of lookahead to decode
    // the last real code. In this case we send two empty static blocks instead
    // of one. (There are no problems if the previous block is stored or fixed.)
    // To simplify the code, we assume the worst case of last real code encoded
    // on one bit only.
    function _tr_align() {
        send_bits(STATIC_TREES << 1, 3);
        send_code(END_BLOCK, StaticTree.static_ltree);

        bi_flush();

        // Of the 10 bits for the empty block, we have already sent
        // (10 - bi_valid) bits. The lookahead for the last real code (before
        // the EOB of the previous block) was thus at least one plus the length
        // of the EOB plus what we have just sent of the empty static block.
        if (1 + last_eob_len + 10 - bi_valid < 9) {
            send_bits(STATIC_TREES << 1, 3);
            send_code(END_BLOCK, StaticTree.static_ltree);
            bi_flush();
        }
        last_eob_len = 7;
    }

    // Save the match info and tally the frequency counts. Return true if
    // the current block must be flushed.
    function _tr_tally(dist, // distance of matched string
    lc // match length-MIN_MATCH or unmatched char (if dist==0)
    ) {
        var out_length, in_length, dcode;
        that.pending_buf[d_buf + last_lit * 2] = (dist >>> 8) & 0xff;
        that.pending_buf[d_buf + last_lit * 2 + 1] = dist & 0xff;

        that.pending_buf[l_buf + last_lit] = lc & 0xff;
        last_lit++;

        if (dist === 0) {
            // lc is the unmatched char
            dyn_ltree[lc * 2]++;
        } else {
            matches++;
            // Here, lc is the match length - MIN_MATCH
            dist--; // dist = match distance - 1
            dyn_ltree[(Tree._length_code[lc] + LITERALS + 1) * 2]++;
            dyn_dtree[Tree.d_code(dist) * 2]++;
        }

        if ((last_lit & 0x1fff) === 0 && level > 2) {
            // Compute an upper bound for the compressed length
            out_length = last_lit * 8;
            in_length = strstart - block_start;
            for (dcode = 0; dcode < D_CODES; dcode++) {
                out_length += dyn_dtree[dcode * 2] * (5 + Tree.extra_dbits[dcode]);
            }
            out_length >>>= 3;
            if ((matches < Math.floor(last_lit / 2)) && out_length < Math.floor(in_length / 2))
                return true;
        }

        return (last_lit == lit_bufsize - 1);
        // We avoid equality with lit_bufsize because of wraparound at 64K
        // on 16 bit machines and because stored blocks are restricted to
        // 64K-1 bytes.
    }

    // Send the block data compressed using the given Huffman trees
    function compress_block(ltree, dtree) {
        var dist; // distance of matched string
        var lc; // match length or unmatched char (if dist === 0)
        var lx = 0; // running index in l_buf
        var code; // the code to send
        var extra; // number of extra bits to send

        if (last_lit !== 0) {
            do {
                dist = ((that.pending_buf[d_buf + lx * 2] << 8) & 0xff00) | (that.pending_buf[d_buf + lx * 2 + 1] & 0xff);
                lc = (that.pending_buf[l_buf + lx]) & 0xff;
                lx++;

                if (dist === 0) {
                    send_code(lc, ltree); // send a literal byte
                } else {
                    // Here, lc is the match length - MIN_MATCH
                    code = Tree._length_code[lc];

                    send_code(code + LITERALS + 1, ltree); // send the length
                    // code
                    extra = Tree.extra_lbits[code];
                    if (extra !== 0) {
                        lc -= Tree.base_length[code];
                        send_bits(lc, extra); // send the extra length bits
                    }
                    dist--; // dist is now the match distance - 1
                    code = Tree.d_code(dist);

                    send_code(code, dtree); // send the distance code
                    extra = Tree.extra_dbits[code];
                    if (extra !== 0) {
                        dist -= Tree.base_dist[code];
                        send_bits(dist, extra); // send the extra distance bits
                    }
                } // literal or match pair ?

                // Check that the overlay between pending_buf and d_buf+l_buf is
                // ok:
            } while (lx < last_lit);
        }

        send_code(END_BLOCK, ltree);
        last_eob_len = ltree[END_BLOCK * 2 + 1];
    }

    // Flush the bit buffer and align the output on a byte boundary
    function bi_windup() {
        if (bi_valid > 8) {
            put_short(bi_buf);
        } else if (bi_valid > 0) {
            put_byte(bi_buf & 0xff);
        }
        bi_buf = 0;
        bi_valid = 0;
    }

    // Copy a stored block, storing first the length and its
    // one's complement if requested.
    function copy_block(buf, // the input data
    len, // its length
    header // true if block header must be written
    ) {
        bi_windup(); // align on byte boundary
        last_eob_len = 8; // enough lookahead for inflate

        if (header) {
            put_short(len);
            put_short(~len);
        }

        that.pending_buf.set(window.subarray(buf, buf + len), that.pending);
        that.pending += len;
    }

    // Send a stored block
    function _tr_stored_block(buf, // input block
    stored_len, // length of input block
    eof // true if this is the last block for a file
    ) {
        send_bits((STORED_BLOCK << 1) + (eof ? 1 : 0), 3); // send block type
        copy_block(buf, stored_len, true); // with header
    }

    // Determine the best encoding for the current block: dynamic trees, static
    // trees or store, and output the encoded block to the zip file.
    function _tr_flush_block(buf, // input block, or NULL if too old
    stored_len, // length of input block
    eof // true if this is the last block for a file
    ) {
        var opt_lenb, static_lenb;// opt_len and static_len in bytes
        var max_blindex = 0; // index of last bit length code of non zero freq

        // Build the Huffman trees unless a stored block is forced
        if (level > 0) {
            // Construct the literal and distance trees
            l_desc.build_tree(that);

            d_desc.build_tree(that);

            // At this point, opt_len and static_len are the total bit lengths
            // of
            // the compressed block data, excluding the tree representations.

            // Build the bit length tree for the above two trees, and get the
            // index
            // in bl_order of the last bit length code to send.
            max_blindex = build_bl_tree();

            // Determine the best encoding. Compute first the block length in
            // bytes
            opt_lenb = (that.opt_len + 3 + 7) >>> 3;
            static_lenb = (that.static_len + 3 + 7) >>> 3;

            if (static_lenb <= opt_lenb)
                opt_lenb = static_lenb;
        } else {
            opt_lenb = static_lenb = stored_len + 5; // force a stored block
        }

        if ((stored_len + 4 <= opt_lenb) && buf != -1) {
            // 4: two words for the lengths
            // The test buf != NULL is only necessary if LIT_BUFSIZE > WSIZE.
            // Otherwise we can't have processed more than WSIZE input bytes
            // since
            // the last block flush, because compression would have been
            // successful. If LIT_BUFSIZE <= WSIZE, it is never too late to
            // transform a block into a stored block.
            _tr_stored_block(buf, stored_len, eof);
        } else if (static_lenb == opt_lenb) {
            send_bits((STATIC_TREES << 1) + (eof ? 1 : 0), 3);
            compress_block(StaticTree.static_ltree, StaticTree.static_dtree);
        } else {
            send_bits((DYN_TREES << 1) + (eof ? 1 : 0), 3);
            send_all_trees(l_desc.max_code + 1, d_desc.max_code + 1, max_blindex + 1);
            compress_block(dyn_ltree, dyn_dtree);
        }

        // The above check is made mod 2^32, for files larger than 512 MB
        // and uLong implemented on 32 bits.

        init_block();

        if (eof) {
            bi_windup();
        }
    }

    function flush_block_only(eof) {
        _tr_flush_block(block_start >= 0 ? block_start : -1, strstart - block_start, eof);
        block_start = strstart;
        strm.flush_pending();
    }

    // Fill the window when the lookahead becomes insufficient.
    // Updates strstart and lookahead.
    //
    // IN assertion: lookahead < MIN_LOOKAHEAD
    // OUT assertions: strstart <= window_size-MIN_LOOKAHEAD
    // At least one byte has been read, or avail_in === 0; reads are
    // performed for at least two bytes (required for the zip translate_eol
    // option -- not supported here).
    function fill_window() {
        var n, m;
        var p;
        var more; // Amount of free space at the end of the window.

        do {
            more = (window_size - lookahead - strstart);

            // Deal with !@#$% 64K limit:
            if (more === 0 && strstart === 0 && lookahead === 0) {
                more = w_size;
            } else if (more == -1) {
                // Very unlikely, but possible on 16 bit machine if strstart ==
                // 0
                // and lookahead == 1 (input done one byte at time)
                more--;

                // If the window is almost full and there is insufficient
                // lookahead,
                // move the upper half to the lower one to make room in the
                // upper half.
            } else if (strstart >= w_size + w_size - MIN_LOOKAHEAD) {
                window.set(window.subarray(w_size, w_size + w_size), 0);

                match_start -= w_size;
                strstart -= w_size; // we now have strstart >= MAX_DIST
                block_start -= w_size;

                // Slide the hash table (could be avoided with 32 bit values
                // at the expense of memory usage). We slide even when level ==
                // 0
                // to keep the hash table consistent if we switch back to level
                // > 0
                // later. (Using level 0 permanently is not an optimal usage of
                // zlib, so we don't care about this pathological case.)

                n = hash_size;
                p = n;
                do {
                    m = (head[--p] & 0xffff);
                    head[p] = (m >= w_size ? m - w_size : 0);
                } while (--n !== 0);

                n = w_size;
                p = n;
                do {
                    m = (prev[--p] & 0xffff);
                    prev[p] = (m >= w_size ? m - w_size : 0);
                    // If n is not on any hash chain, prev[n] is garbage but
                    // its value will never be used.
                } while (--n !== 0);
                more += w_size;
            }

            if (strm.avail_in === 0)
                return;

            // If there was no sliding:
            // strstart <= WSIZE+MAX_DIST-1 && lookahead <= MIN_LOOKAHEAD - 1 &&
            // more == window_size - lookahead - strstart
            // => more >= window_size - (MIN_LOOKAHEAD-1 + WSIZE + MAX_DIST-1)
            // => more >= window_size - 2*WSIZE + 2
            // In the BIG_MEM or MMAP case (not yet supported),
            // window_size == input_size + MIN_LOOKAHEAD &&
            // strstart + s->lookahead <= input_size => more >= MIN_LOOKAHEAD.
            // Otherwise, window_size == 2*WSIZE so more >= 2.
            // If there was sliding, more >= WSIZE. So in all cases, more >= 2.

            n = strm.read_buf(window, strstart + lookahead, more);
            lookahead += n;

            // Initialize the hash value now that we have some input:
            if (lookahead >= MIN_MATCH) {
                ins_h = window[strstart] & 0xff;
                ins_h = (((ins_h) << hash_shift) ^ (window[strstart + 1] & 0xff)) & hash_mask;
            }
            // If the whole input has less than MIN_MATCH bytes, ins_h is
            // garbage,
            // but this is not important since only literal bytes will be
            // emitted.
        } while (lookahead < MIN_LOOKAHEAD && strm.avail_in !== 0);
    }

    // Copy without compression as much as possible from the input stream,
    // return
    // the current block state.
    // This function does not insert new strings in the dictionary since
    // uncompressible data is probably not useful. This function is used
    // only for the level=0 compression option.
    // NOTE: this function should be optimized to avoid extra copying from
    // window to pending_buf.
    function deflate_stored(flush) {
        // Stored blocks are limited to 0xffff bytes, pending_buf is limited
        // to pending_buf_size, and each stored block has a 5 byte header:

        var max_block_size = 0xffff;
        var max_start;

        if (max_block_size > pending_buf_size - 5) {
            max_block_size = pending_buf_size - 5;
        }

        // Copy as much as possible from input to output:
        while (true) {
            // Fill the window as much as possible:
            if (lookahead <= 1) {
                fill_window();
                if (lookahead === 0 && flush == Z_NO_FLUSH)
                    return NeedMore;
                if (lookahead === 0)
                    break; // flush the current block
            }

            strstart += lookahead;
            lookahead = 0;

            // Emit a stored block if pending_buf will be full:
            max_start = block_start + max_block_size;
            if (strstart === 0 || strstart >= max_start) {
                // strstart === 0 is possible when wraparound on 16-bit machine
                lookahead = (strstart - max_start);
                strstart = max_start;

                flush_block_only(false);
                if (strm.avail_out === 0)
                    return NeedMore;

            }

            // Flush if we may have to slide, otherwise block_start may become
            // negative and the data will be gone:
            if (strstart - block_start >= w_size - MIN_LOOKAHEAD) {
                flush_block_only(false);
                if (strm.avail_out === 0)
                    return NeedMore;
            }
        }

        flush_block_only(flush == Z_FINISH);
        if (strm.avail_out === 0)
            return (flush == Z_FINISH) ? FinishStarted : NeedMore;

        return flush == Z_FINISH ? FinishDone : BlockDone;
    }

    function longest_match(cur_match) {
        var chain_length = max_chain_length; // max hash chain length
        var scan = strstart; // current string
        var match; // matched string
        var len; // length of current match
        var best_len = prev_length; // best match length so far
        var limit = strstart > (w_size - MIN_LOOKAHEAD) ? strstart - (w_size - MIN_LOOKAHEAD) : 0;
        var _nice_match = nice_match;

        // Stop when cur_match becomes <= limit. To simplify the code,
        // we prevent matches with the string of window index 0.

        var wmask = w_mask;

        var strend = strstart + MAX_MATCH;
        var scan_end1 = window[scan + best_len - 1];
        var scan_end = window[scan + best_len];

        // The code is optimized for HASH_BITS >= 8 and MAX_MATCH-2 multiple of
        // 16.
        // It is easy to get rid of this optimization if necessary.

        // Do not waste too much time if we already have a good match:
        if (prev_length >= good_match) {
            chain_length >>= 2;
        }

        // Do not look for matches beyond the end of the input. This is
        // necessary
        // to make deflate deterministic.
        if (_nice_match > lookahead)
            _nice_match = lookahead;

        do {
            match = cur_match;

            // Skip to next match if the match length cannot increase
            // or if the match length is less than 2:
            if (window[match + best_len] != scan_end || window[match + best_len - 1] != scan_end1 || window[match] != window[scan]
                    || window[++match] != window[scan + 1])
                continue;

            // The check at best_len-1 can be removed because it will be made
            // again later. (This heuristic is not always a win.)
            // It is not necessary to compare scan[2] and match[2] since they
            // are always equal when the other bytes match, given that
            // the hash keys are equal and that HASH_BITS >= 8.
            scan += 2;
            match++;

            // We check for insufficient lookahead only every 8th comparison;
            // the 256th check will be made at strstart+258.
            do {
            } while (window[++scan] == window[++match] && window[++scan] == window[++match] && window[++scan] == window[++match]
                    && window[++scan] == window[++match] && window[++scan] == window[++match] && window[++scan] == window[++match]
                    && window[++scan] == window[++match] && window[++scan] == window[++match] && scan < strend);

            len = MAX_MATCH - (strend - scan);
            scan = strend - MAX_MATCH;

            if (len > best_len) {
                match_start = cur_match;
                best_len = len;
                if (len >= _nice_match)
                    break;
                scan_end1 = window[scan + best_len - 1];
                scan_end = window[scan + best_len];
            }

        } while ((cur_match = (prev[cur_match & wmask] & 0xffff)) > limit && --chain_length !== 0);

        if (best_len <= lookahead)
            return best_len;
        return lookahead;
    }

    // Compress as much as possible from the input stream, return the current
    // block state.
    // This function does not perform lazy evaluation of matches and inserts
    // new strings in the dictionary only for unmatched strings or for short
    // matches. It is used only for the fast compression options.
    function deflate_fast(flush) {
        // short hash_head = 0; // head of the hash chain
        var hash_head = 0; // head of the hash chain
        var bflush; // set if current block must be flushed

        while (true) {
            // Make sure that we always have enough lookahead, except
            // at the end of the input file. We need MAX_MATCH bytes
            // for the next match, plus MIN_MATCH bytes to insert the
            // string following the next match.
            if (lookahead < MIN_LOOKAHEAD) {
                fill_window();
                if (lookahead < MIN_LOOKAHEAD && flush == Z_NO_FLUSH) {
                    return NeedMore;
                }
                if (lookahead === 0)
                    break; // flush the current block
            }

            // Insert the string window[strstart .. strstart+2] in the
            // dictionary, and set hash_head to the head of the hash chain:
            if (lookahead >= MIN_MATCH) {
                ins_h = (((ins_h) << hash_shift) ^ (window[(strstart) + (MIN_MATCH - 1)] & 0xff)) & hash_mask;

                // prev[strstart&w_mask]=hash_head=head[ins_h];
                hash_head = (head[ins_h] & 0xffff);
                prev[strstart & w_mask] = head[ins_h];
                head[ins_h] = strstart;
            }

            // Find the longest match, discarding those <= prev_length.
            // At this point we have always match_length < MIN_MATCH

            if (hash_head !== 0 && ((strstart - hash_head) & 0xffff) <= w_size - MIN_LOOKAHEAD) {
                // To simplify the code, we prevent matches with the string
                // of window index 0 (in particular we have to avoid a match
                // of the string with itself at the start of the input file).
                if (strategy != Z_HUFFMAN_ONLY) {
                    match_length = longest_match(hash_head);
                }
                // longest_match() sets match_start
            }
            if (match_length >= MIN_MATCH) {
                // check_match(strstart, match_start, match_length);

                bflush = _tr_tally(strstart - match_start, match_length - MIN_MATCH);

                lookahead -= match_length;

                // Insert new strings in the hash table only if the match length
                // is not too large. This saves time but degrades compression.
                if (match_length <= max_lazy_match && lookahead >= MIN_MATCH) {
                    match_length--; // string at strstart already in hash table
                    do {
                        strstart++;

                        ins_h = ((ins_h << hash_shift) ^ (window[(strstart) + (MIN_MATCH - 1)] & 0xff)) & hash_mask;
                        // prev[strstart&w_mask]=hash_head=head[ins_h];
                        hash_head = (head[ins_h] & 0xffff);
                        prev[strstart & w_mask] = head[ins_h];
                        head[ins_h] = strstart;

                        // strstart never exceeds WSIZE-MAX_MATCH, so there are
                        // always MIN_MATCH bytes ahead.
                    } while (--match_length !== 0);
                    strstart++;
                } else {
                    strstart += match_length;
                    match_length = 0;
                    ins_h = window[strstart] & 0xff;

                    ins_h = (((ins_h) << hash_shift) ^ (window[strstart + 1] & 0xff)) & hash_mask;
                    // If lookahead < MIN_MATCH, ins_h is garbage, but it does
                    // not
                    // matter since it will be recomputed at next deflate call.
                }
            } else {
                // No match, output a literal byte

                bflush = _tr_tally(0, window[strstart] & 0xff);
                lookahead--;
                strstart++;
            }
            if (bflush) {

                flush_block_only(false);
                if (strm.avail_out === 0)
                    return NeedMore;
            }
        }

        flush_block_only(flush == Z_FINISH);
        if (strm.avail_out === 0) {
            if (flush == Z_FINISH)
                return FinishStarted;
            else
                return NeedMore;
        }
        return flush == Z_FINISH ? FinishDone : BlockDone;
    }

    // Same as above, but achieves better compression. We use a lazy
    // evaluation for matches: a match is finally adopted only if there is
    // no better match at the next window position.
    function deflate_slow(flush) {
        // short hash_head = 0; // head of hash chain
        var hash_head = 0; // head of hash chain
        var bflush; // set if current block must be flushed
        var max_insert;

        // Process the input block.
        while (true) {
            // Make sure that we always have enough lookahead, except
            // at the end of the input file. We need MAX_MATCH bytes
            // for the next match, plus MIN_MATCH bytes to insert the
            // string following the next match.

            if (lookahead < MIN_LOOKAHEAD) {
                fill_window();
                if (lookahead < MIN_LOOKAHEAD && flush == Z_NO_FLUSH) {
                    return NeedMore;
                }
                if (lookahead === 0)
                    break; // flush the current block
            }

            // Insert the string window[strstart .. strstart+2] in the
            // dictionary, and set hash_head to the head of the hash chain:

            if (lookahead >= MIN_MATCH) {
                ins_h = (((ins_h) << hash_shift) ^ (window[(strstart) + (MIN_MATCH - 1)] & 0xff)) & hash_mask;
                // prev[strstart&w_mask]=hash_head=head[ins_h];
                hash_head = (head[ins_h] & 0xffff);
                prev[strstart & w_mask] = head[ins_h];
                head[ins_h] = strstart;
            }

            // Find the longest match, discarding those <= prev_length.
            prev_length = match_length;
            prev_match = match_start;
            match_length = MIN_MATCH - 1;

            if (hash_head !== 0 && prev_length < max_lazy_match && ((strstart - hash_head) & 0xffff) <= w_size - MIN_LOOKAHEAD) {
                // To simplify the code, we prevent matches with the string
                // of window index 0 (in particular we have to avoid a match
                // of the string with itself at the start of the input file).

                if (strategy != Z_HUFFMAN_ONLY) {
                    match_length = longest_match(hash_head);
                }
                // longest_match() sets match_start

                if (match_length <= 5 && (strategy == Z_FILTERED || (match_length == MIN_MATCH && strstart - match_start > 4096))) {

                    // If prev_match is also MIN_MATCH, match_start is garbage
                    // but we will ignore the current match anyway.
                    match_length = MIN_MATCH - 1;
                }
            }

            // If there was a match at the previous step and the current
            // match is not better, output the previous match:
            if (prev_length >= MIN_MATCH && match_length <= prev_length) {
                max_insert = strstart + lookahead - MIN_MATCH;
                // Do not insert strings in hash table beyond this.

                // check_match(strstart-1, prev_match, prev_length);

                bflush = _tr_tally(strstart - 1 - prev_match, prev_length - MIN_MATCH);

                // Insert in hash table all strings up to the end of the match.
                // strstart-1 and strstart are already inserted. If there is not
                // enough lookahead, the last two strings are not inserted in
                // the hash table.
                lookahead -= prev_length - 1;
                prev_length -= 2;
                do {
                    if (++strstart <= max_insert) {
                        ins_h = (((ins_h) << hash_shift) ^ (window[(strstart) + (MIN_MATCH - 1)] & 0xff)) & hash_mask;
                        // prev[strstart&w_mask]=hash_head=head[ins_h];
                        hash_head = (head[ins_h] & 0xffff);
                        prev[strstart & w_mask] = head[ins_h];
                        head[ins_h] = strstart;
                    }
                } while (--prev_length !== 0);
                match_available = 0;
                match_length = MIN_MATCH - 1;
                strstart++;

                if (bflush) {
                    flush_block_only(false);
                    if (strm.avail_out === 0)
                        return NeedMore;
                }
            } else if (match_available !== 0) {

                // If there was no match at the previous position, output a
                // single literal. If there was a match but the current match
                // is longer, truncate the previous match to a single literal.

                bflush = _tr_tally(0, window[strstart - 1] & 0xff);

                if (bflush) {
                    flush_block_only(false);
                }
                strstart++;
                lookahead--;
                if (strm.avail_out === 0)
                    return NeedMore;
            } else {
                // There is no previous match to compare with, wait for
                // the next step to decide.

                match_available = 1;
                strstart++;
                lookahead--;
            }
        }

        if (match_available !== 0) {
            bflush = _tr_tally(0, window[strstart - 1] & 0xff);
            match_available = 0;
        }
        flush_block_only(flush == Z_FINISH);

        if (strm.avail_out === 0) {
            if (flush == Z_FINISH)
                return FinishStarted;
            else
                return NeedMore;
        }

        return flush == Z_FINISH ? FinishDone : BlockDone;
    }

    function deflateReset(strm) {
        strm.total_in = strm.total_out = 0;
        strm.msg = null; //
        
        that.pending = 0;
        that.pending_out = 0;

        status = BUSY_STATE;

        last_flush = Z_NO_FLUSH;

        tr_init();
        lm_init();
        return Z_OK;
    }

    that.deflateInit = function(strm, _level, bits, _method, memLevel, _strategy) {
        if (!_method)
            _method = Z_DEFLATED;
        if (!memLevel)
            memLevel = DEF_MEM_LEVEL;
        if (!_strategy)
            _strategy = Z_DEFAULT_STRATEGY;

        // byte[] my_version=ZLIB_VERSION;

        //
        // if (!version || version[0] != my_version[0]
        // || stream_size != sizeof(z_stream)) {
        // return Z_VERSION_ERROR;
        // }

        strm.msg = null;

        if (_level == Z_DEFAULT_COMPRESSION)
            _level = 6;

        if (memLevel < 1 || memLevel > MAX_MEM_LEVEL || _method != Z_DEFLATED || bits < 9 || bits > 15 || _level < 0 || _level > 9 || _strategy < 0
                || _strategy > Z_HUFFMAN_ONLY) {
            return Z_STREAM_ERROR;
        }

        strm.dstate = that;

        w_bits = bits;
        w_size = 1 << w_bits;
        w_mask = w_size - 1;

        hash_bits = memLevel + 7;
        hash_size = 1 << hash_bits;
        hash_mask = hash_size - 1;
        hash_shift = Math.floor((hash_bits + MIN_MATCH - 1) / MIN_MATCH);

        window = new Uint8Array(w_size * 2);
        prev = [];
        head = [];

        lit_bufsize = 1 << (memLevel + 6); // 16K elements by default

        // We overlay pending_buf and d_buf+l_buf. This works since the average
        // output size for (length,distance) codes is <= 24 bits.
        that.pending_buf = new Uint8Array(lit_bufsize * 4);
        pending_buf_size = lit_bufsize * 4;

        d_buf = Math.floor(lit_bufsize / 2);
        l_buf = (1 + 2) * lit_bufsize;

        level = _level;

        strategy = _strategy;
        method = _method & 0xff;

        return deflateReset(strm);
    };

    that.deflateEnd = function() {
        if (status != INIT_STATE && status != BUSY_STATE && status != FINISH_STATE) {
            return Z_STREAM_ERROR;
        }
        // Deallocate in reverse order of allocations:
        that.pending_buf = null;
        head = null;
        prev = null;
        window = null;
        // free
        that.dstate = null;
        return status == BUSY_STATE ? Z_DATA_ERROR : Z_OK;
    };

    that.deflateParams = function(strm, _level, _strategy) {
        var err = Z_OK;

        if (_level == Z_DEFAULT_COMPRESSION) {
            _level = 6;
        }
        if (_level < 0 || _level > 9 || _strategy < 0 || _strategy > Z_HUFFMAN_ONLY) {
            return Z_STREAM_ERROR;
        }

        if (config_table[level].func != config_table[_level].func && strm.total_in !== 0) {
            // Flush the last buffer:
            err = strm.deflate(Z_PARTIAL_FLUSH);
        }

        if (level != _level) {
            level = _level;
            max_lazy_match = config_table[level].max_lazy;
            good_match = config_table[level].good_length;
            nice_match = config_table[level].nice_length;
            max_chain_length = config_table[level].max_chain;
        }
        strategy = _strategy;
        return err;
    };

    that.deflateSetDictionary = function(strm, dictionary, dictLength) {
        var length = dictLength;
        var n, index = 0;

        if (!dictionary || status != INIT_STATE)
            return Z_STREAM_ERROR;

        if (length < MIN_MATCH)
            return Z_OK;
        if (length > w_size - MIN_LOOKAHEAD) {
            length = w_size - MIN_LOOKAHEAD;
            index = dictLength - length; // use the tail of the dictionary
        }
        window.set(dictionary.subarray(index, index + length), 0);

        strstart = length;
        block_start = length;

        // Insert all strings in the hash table (except for the last two bytes).
        // s->lookahead stays null, so s->ins_h will be recomputed at the next
        // call of fill_window.

        ins_h = window[0] & 0xff;
        ins_h = (((ins_h) << hash_shift) ^ (window[1] & 0xff)) & hash_mask;

        for (n = 0; n <= length - MIN_MATCH; n++) {
            ins_h = (((ins_h) << hash_shift) ^ (window[(n) + (MIN_MATCH - 1)] & 0xff)) & hash_mask;
            prev[n & w_mask] = head[ins_h];
            head[ins_h] = n;
        }
        return Z_OK;
    };

    that.deflate = function(_strm, flush) {
        var i, header, level_flags, old_flush, bstate;

        if (flush > Z_FINISH || flush < 0) {
            return Z_STREAM_ERROR;
        }

        if (!_strm.next_out || (!_strm.next_in && _strm.avail_in !== 0) || (status == FINISH_STATE && flush != Z_FINISH)) {
            _strm.msg = z_errmsg[Z_NEED_DICT - (Z_STREAM_ERROR)];
            return Z_STREAM_ERROR;
        }
        if (_strm.avail_out === 0) {
            _strm.msg = z_errmsg[Z_NEED_DICT - (Z_BUF_ERROR)];
            return Z_BUF_ERROR;
        }

        strm = _strm; // just in case
        old_flush = last_flush;
        last_flush = flush;

        // Write the zlib header
        if (status == INIT_STATE) {
            header = (Z_DEFLATED + ((w_bits - 8) << 4)) << 8;
            level_flags = ((level - 1) & 0xff) >> 1;

            if (level_flags > 3)
                level_flags = 3;
            header |= (level_flags << 6);
            if (strstart !== 0)
                header |= PRESET_DICT;
            header += 31 - (header % 31);

            status = BUSY_STATE;
            putShortMSB(header);
        }

        // Flush as much pending output as possible
        if (that.pending !== 0) {
            strm.flush_pending();
            if (strm.avail_out === 0) {
                // console.log(" avail_out==0");
                // Since avail_out is 0, deflate will be called again with
                // more output space, but possibly with both pending and
                // avail_in equal to zero. There won't be anything to do,
                // but this is not an error situation so make sure we
                // return OK instead of BUF_ERROR at next call of deflate:
                last_flush = -1;
                return Z_OK;
            }

            // Make sure there is something to do and avoid duplicate
            // consecutive
            // flushes. For repeated and useless calls with Z_FINISH, we keep
            // returning Z_STREAM_END instead of Z_BUFF_ERROR.
        } else if (strm.avail_in === 0 && flush <= old_flush && flush != Z_FINISH) {
            strm.msg = z_errmsg[Z_NEED_DICT - (Z_BUF_ERROR)];
            return Z_BUF_ERROR;
        }

        // User must not provide more input after the first FINISH:
        if (status == FINISH_STATE && strm.avail_in !== 0) {
            _strm.msg = z_errmsg[Z_NEED_DICT - (Z_BUF_ERROR)];
            return Z_BUF_ERROR;
        }

        // Start a new block or continue the current one.
        if (strm.avail_in !== 0 || lookahead !== 0 || (flush != Z_NO_FLUSH && status != FINISH_STATE)) {
            bstate = -1;
            switch (config_table[level].func) {
            case STORED:
                bstate = deflate_stored(flush);
                break;
            case FAST:
                bstate = deflate_fast(flush);
                break;
            case SLOW:
                bstate = deflate_slow(flush);
                break;
            default:
            }

            if (bstate == FinishStarted || bstate == FinishDone) {
                status = FINISH_STATE;
            }
            if (bstate == NeedMore || bstate == FinishStarted) {
                if (strm.avail_out === 0) {
                    last_flush = -1; // avoid BUF_ERROR next call, see above
                }
                return Z_OK;
                // If flush != Z_NO_FLUSH && avail_out === 0, the next call
                // of deflate should use the same flush parameter to make sure
                // that the flush is complete. So we don't have to output an
                // empty block here, this will be done at next call. This also
                // ensures that for a very small output buffer, we emit at most
                // one empty block.
            }

            if (bstate == BlockDone) {
                if (flush == Z_PARTIAL_FLUSH) {
                    _tr_align();
                } else { // FULL_FLUSH or SYNC_FLUSH
                    _tr_stored_block(0, 0, false);
                    // For a full flush, this empty block will be recognized
                    // as a special marker by inflate_sync().
                    if (flush == Z_FULL_FLUSH) {
                        // state.head[s.hash_size-1]=0;
                        for (i = 0; i < hash_size/*-1*/; i++)
                            // forget history
                            head[i] = 0;
                    }
                }
                strm.flush_pending();
                if (strm.avail_out === 0) {
                    last_flush = -1; // avoid BUF_ERROR at next call, see above
                    return Z_OK;
                }
            }
        }

        if (flush != Z_FINISH)
            return Z_OK;
        return Z_STREAM_END;
    };
}

// ZStream

function ZStream() {
    var that = this;
    that.next_in_index = 0;
    that.next_out_index = 0;
    // that.next_in; // next input byte
    that.avail_in = 0; // number of bytes available at next_in
    that.total_in = 0; // total nb of input bytes read so far
    // that.next_out; // next output byte should be put there
    that.avail_out = 0; // remaining free space at next_out
    that.total_out = 0; // total nb of bytes output so far
    // that.msg;
    // that.dstate;
}

ZStream.prototype = {
    deflateInit : function(level, bits) {
        var that = this;
        that.dstate = new Deflate();
        if (!bits)
            bits = MAX_BITS;
        return that.dstate.deflateInit(that, level, bits);
    },

    deflate : function(flush) {
        var that = this;
        if (!that.dstate) {
            return Z_STREAM_ERROR;
        }
        return that.dstate.deflate(that, flush);
    },

    deflateEnd : function() {
        var that = this;
        if (!that.dstate)
            return Z_STREAM_ERROR;
        var ret = that.dstate.deflateEnd();
        that.dstate = null;
        return ret;
    },

    deflateParams : function(level, strategy) {
        var that = this;
        if (!that.dstate)
            return Z_STREAM_ERROR;
        return that.dstate.deflateParams(that, level, strategy);
    },

    deflateSetDictionary : function(dictionary, dictLength) {
        var that = this;
        if (!that.dstate)
            return Z_STREAM_ERROR;
        return that.dstate.deflateSetDictionary(that, dictionary, dictLength);
    },

    // Read a new buffer from the current input stream, update the
    // total number of bytes read. All deflate() input goes through
    // this function so some applications may wish to modify it to avoid
    // allocating a large strm->next_in buffer and copying from it.
    // (See also flush_pending()).
    read_buf : function(buf, start, size) {
        var that = this;
        var len = that.avail_in;
        if (len > size)
            len = size;
        if (len === 0)
            return 0;
        that.avail_in -= len;
        buf.set(that.next_in.subarray(that.next_in_index, that.next_in_index + len), start);
        that.next_in_index += len;
        that.total_in += len;
        return len;
    },

    // Flush as much pending output as possible. All deflate() output goes
    // through this function so some applications may wish to modify it
    // to avoid allocating a large strm->next_out buffer and copying into it.
    // (See also read_buf()).
    flush_pending : function() {
        var that = this;
        var len = that.dstate.pending;

        if (len > that.avail_out)
            len = that.avail_out;
        if (len === 0)
            return;

        // if (that.dstate.pending_buf.length <= that.dstate.pending_out || that.next_out.length <= that.next_out_index
        // || that.dstate.pending_buf.length < (that.dstate.pending_out + len) || that.next_out.length < (that.next_out_index +
        // len)) {
        // console.log(that.dstate.pending_buf.length + ", " + that.dstate.pending_out + ", " + that.next_out.length + ", " +
        // that.next_out_index + ", " + len);
        // console.log("avail_out=" + that.avail_out);
        // }

        that.next_out.set(that.dstate.pending_buf.subarray(that.dstate.pending_out, that.dstate.pending_out + len), that.next_out_index);

        that.next_out_index += len;
        that.dstate.pending_out += len;
        that.total_out += len;
        that.avail_out -= len;
        that.dstate.pending -= len;
        if (that.dstate.pending === 0) {
            that.dstate.pending_out = 0;
        }
    }
};

// Deflater

function Deflater(level) {
    var that = this;
    var z = new ZStream();
    var bufsize = 512;
    var flush = Z_NO_FLUSH;
    var buf = new Uint8Array(bufsize);

    if (typeof level == "undefined")
        level = Z_DEFAULT_COMPRESSION;
    z.deflateInit(level);
    z.next_out = buf;

    that.append = function(data, onprogress) {
        var err, buffers = [], lastIndex = 0, bufferIndex = 0, bufferSize = 0, array;
        if (!data.length)
            return;
        z.next_in_index = 0;
        z.next_in = data;
        z.avail_in = data.length;
        do {
            z.next_out_index = 0;
            z.avail_out = bufsize;
            err = z.deflate(flush);
            if (err != Z_OK)
                throw "deflating: " + z.msg;
            if (z.next_out_index)
                if (z.next_out_index == bufsize)
                    buffers.push(new Uint8Array(buf));
                else
                    buffers.push(new Uint8Array(buf.subarray(0, z.next_out_index)));
            bufferSize += z.next_out_index;
            if (onprogress && z.next_in_index > 0 && z.next_in_index != lastIndex) {
                onprogress(z.next_in_index);
                lastIndex = z.next_in_index;
            }
        } while (z.avail_in > 0 || z.avail_out === 0);
        array = new Uint8Array(bufferSize);
        buffers.forEach(function(chunk) {
            array.set(chunk, bufferIndex);
            bufferIndex += chunk.length;
        });
        return array;
    };
    that.flush = function() {
        var err, buffers = [], bufferIndex = 0, bufferSize = 0, array;
        do {
            z.next_out_index = 0;
            z.avail_out = bufsize;
            err = z.deflate(Z_FINISH);
            if (err != Z_STREAM_END && err != Z_OK)
                throw "deflating: " + z.msg;
            if (bufsize - z.avail_out > 0)
                buffers.push(new Uint8Array(buf.subarray(0, z.next_out_index)));
            bufferSize += z.next_out_index;
        } while (z.avail_in > 0 || z.avail_out === 0);
        z.deflateEnd();
        array = new Uint8Array(bufferSize);
        buffers.forEach(function(chunk) {
            array.set(chunk, bufferIndex);
            bufferIndex += chunk.length;
        });
        return array;
    };
}


/*
 * Copyright (c) 2012 chick307 <chick307@gmail.com>
 *
 * Licensed under the MIT License.
 * http://opensource.org/licenses/mit-license
 */

var fVoid = function(global, callback) {
	if (typeof module === 'object') {
		module.exports = callback();
	} else if (typeof define === 'function') {
		define(callback);
	} else {
		global.adler32cs = callback();
	}
}(this, function() {
	var _hasArrayBuffer = typeof ArrayBuffer === 'function' &&
		typeof Uint8Array === 'function';

	var _Buffer = null, _isBuffer = (function() {
		if (!_hasArrayBuffer)
			return function _isBuffer() { return false };

		try {
			var buffer = require('buffer');
			if (typeof buffer.Buffer === 'function')
				_Buffer = buffer.Buffer;
		} catch (error) {}

		return function _isBuffer(value) {
			return value instanceof ArrayBuffer ||
				_Buffer !== null && value instanceof _Buffer;
		};
	}());

	var _utf8ToBinary = (function() {
		if (_Buffer !== null) {
			return function _utf8ToBinary(utf8String) {
				return new _Buffer(utf8String, 'utf8').toString('binary');
			};
		} else {
			return function _utf8ToBinary(utf8String) {
				return unescape(encodeURIComponent(utf8String));
			};
		}
	}());

	var MOD = 65521;

	var _update = function _update(checksum, binaryString) {
		var a = checksum & 0xFFFF, b = checksum >>> 16;
		for (var i = 0, length = binaryString.length; i < length; i++) {
			a = (a + (binaryString.charCodeAt(i) & 0xFF)) % MOD;
			b = (b + a) % MOD;
		}
		return (b << 16 | a) >>> 0;
	};

	var _updateUint8Array = function _updateUint8Array(checksum, uint8Array) {
		var a = checksum & 0xFFFF, b = checksum >>> 16;
		for (var i = 0, length = uint8Array.length, x; i < length; i++) {
			a = (a + uint8Array[i]) % MOD;
			b = (b + a) % MOD;
		}
		return (b << 16 | a) >>> 0
	};

	var exports = {};

	var Adler32 = exports.Adler32 = (function() {
		var ctor = function Adler32(checksum) {
			if (!(this instanceof ctor)) {
				throw new TypeError(
					'Constructor cannot called be as a function.');
			}
			if (!isFinite(checksum = checksum == null ? 1 : +checksum)) {
				throw new Error(
					'First arguments needs to be a finite number.');
			}
			this.checksum = checksum >>> 0;
		};

		var proto = ctor.prototype = {};
		proto.constructor = ctor;

		ctor.from = function(from) {
			from.prototype = proto;
			return from;
		}(function from(binaryString) {
			if (!(this instanceof ctor)) {
				throw new TypeError(
					'Constructor cannot called be as a function.');
			}
			if (binaryString == null)
				throw new Error('First argument needs to be a string.');
			this.checksum = _update(1, binaryString.toString());
		});

		ctor.fromUtf8 = function(fromUtf8) {
			fromUtf8.prototype = proto;
			return fromUtf8;
		}(function fromUtf8(utf8String) {
			if (!(this instanceof ctor)) {
				throw new TypeError(
					'Constructor cannot called be as a function.');
			}
			if (utf8String == null)
				throw new Error('First argument needs to be a string.');
			var binaryString = _utf8ToBinary(utf8String.toString());
			this.checksum = _update(1, binaryString);
		});

		if (_hasArrayBuffer) {
			ctor.fromBuffer = function(fromBuffer) {
				fromBuffer.prototype = proto;
				return fromBuffer;
			}(function fromBuffer(buffer) {
				if (!(this instanceof ctor)) {
					throw new TypeError(
						'Constructor cannot called be as a function.');
				}
				if (!_isBuffer(buffer))
					throw new Error('First argument needs to be ArrayBuffer.');
				var array = new Uint8Array(buffer);
				return this.checksum = _updateUint8Array(1, array);
			});
		}

		proto.update = function update(binaryString) {
			if (binaryString == null)
				throw new Error('First argument needs to be a string.');
			binaryString = binaryString.toString();
			return this.checksum = _update(this.checksum, binaryString);
		};

		proto.updateUtf8 = function updateUtf8(utf8String) {
			if (utf8String == null)
				throw new Error('First argument needs to be a string.');
			var binaryString = _utf8ToBinary(utf8String.toString());
			return this.checksum = _update(this.checksum, binaryString);
		};

		if (_hasArrayBuffer) {
			proto.updateBuffer = function updateBuffer(buffer) {
				if (!_isBuffer(buffer))
					throw new Error('First argument needs to be ArrayBuffer.');
				var array = new Uint8Array(buffer);
				return this.checksum = _updateUint8Array(this.checksum, array);
			};
		}

		proto.clone = function clone() {
			return new Adler32(this.checksum);
		};

		return ctor;
	}());

	exports.from = function from(binaryString) {
		if (binaryString == null)
			throw new Error('First argument needs to be a string.');
		return _update(1, binaryString.toString());
	};

	exports.fromUtf8 = function fromUtf8(utf8String) {
		if (utf8String == null)
			throw new Error('First argument needs to be a string.');
		var binaryString = _utf8ToBinary(utf8String.toString());
		return _update(1, binaryString);
	};

	if (_hasArrayBuffer) {
		exports.fromBuffer = function fromBuffer(buffer) {
			if (!_isBuffer(buffer))
				throw new Error('First argument need to be ArrayBuffer.');
			var array = new Uint8Array(buffer);
			return _updateUint8Array(1, array);
		};
	}

	return exports;
});




var JSZip=function(data,options)
{this.files={};this.root="";if(data)this.load(data,options);}
JSZip.signature={LOCAL_FILE_HEADER:"\x50\x4b\x03\x04",CENTRAL_FILE_HEADER:"\x50\x4b\x01\x02",CENTRAL_DIRECTORY_END:"\x50\x4b\x05\x06",ZIP64_CENTRAL_DIRECTORY_LOCATOR:"\x50\x4b\x06\x07",ZIP64_CENTRAL_DIRECTORY_END:"\x50\x4b\x06\x06",DATA_DESCRIPTOR:"\x50\x4b\x07\x08"};JSZip.defaults={base64:false,binary:false,dir:false,date:null};JSZip.prototype=(function()
{var ZipObject=function(name,data,options)
{this.name=name;this.data=data;this.options=options;};ZipObject.prototype={asText:function()
{return this.options.binary?JSZip.prototype.utf8decode(this.data):this.data;},asBinary:function()
{return this.options.binary?this.data:JSZip.prototype.utf8encode(this.data);}};var decToHex=function(dec,bytes)
{var hex="",i;for(i=0;i<bytes;i++)
{hex+=String.fromCharCode(dec&0xff);dec=dec>>>8;}
return hex;};var extend=function()
{var result={},i,attr;for(i=0;i<arguments.length;i++)
{for(attr in arguments[i])
{if(typeof result[attr]==="undefined")
{result[attr]=arguments[i][attr];}}}
return result;};var prepareFileAttrs=function(o)
{o=o||{};if(o.base64===true&&o.binary==null)o.binary=true;o=extend(o,JSZip.defaults);o.date=o.date||new Date();return o;}
var fileAdd=function(name,data,o)
{var parent=parentFolder(name);if(parent)folderAdd.call(this,parent);o=prepareFileAttrs(o);return this.files[name]={name:name,data:data,options:o};};var parentFolder=function(path)
{if(path.slice(-1)=='/')
{path=path.substring(0,path.length- 1);}
var lastSlash=path.lastIndexOf('/');return(lastSlash>0)?path.substring(0,lastSlash):"";};var folderAdd=function(name)
{if(name.slice(-1)!="/")name+="/";if(!this.files[name])
{var parent=parentFolder(name);if(parent)folderAdd.call(this,parent);fileAdd.call(this,name,'',{dir:true});}
return this.files[name];};var prepareLocalHeaderData=function(file,utfEncodedFileName,compressionType)
{var useUTF8=utfEncodedFileName!==file.name,data=file.data,o=file.options,dosTime,dosDate;dosTime=o.date.getHours();dosTime=dosTime<<6;dosTime=dosTime|o.date.getMinutes();dosTime=dosTime<<5;dosTime=dosTime|o.date.getSeconds()/2;dosDate=o.date.getFullYear()- 1980;dosDate=dosDate<<4;dosDate=dosDate|(o.date.getMonth()+ 1);dosDate=dosDate<<5;dosDate=dosDate|o.date.getDate();if(o.base64===true)data=JSZipBase64.decode(data);if(o.binary===false)data=this.utf8encode(data);var compression=JSZip.compressions[compressionType];var compressedData=compression.compress(data);var header="";header+="\x0A\x00";header+=useUTF8?"\x00\x08":"\x00\x00";header+=compression.magic;header+=decToHex(dosTime,2);header+=decToHex(dosDate,2);header+=decToHex(this.crc32(data),4);header+=decToHex(compressedData.length,4);header+=decToHex(data.length,4);header+=decToHex(utfEncodedFileName.length,2);header+="\x00\x00";return{header:header,compressedData:compressedData};};return{load:function(stream,options)
{throw new Error("Load method is not defined. Is the file jszip-load.js included ?");},filter:function(search)
{var result=[],filename,relativePath,file,fileClone;for(filename in this.files)
{file=this.files[filename];fileClone=new ZipObject(file.name,file.data,extend(file.options));relativePath=filename.slice(this.root.length,filename.length);if(filename.slice(0,this.root.length)===this.root&&search(relativePath,fileClone))
{result.push(fileClone);}}
return result;},file:function(name,data,o)
{if(arguments.length===1)
{if(name instanceof RegExp)
{var regexp=name;return this.filter(function(relativePath,file){return!file.options.dir&&regexp.test(relativePath);});}
else
{return this.filter(function(relativePath,file){return!file.options.dir&&relativePath===name;})[0]||null;}}
else
{name=this.root+name;fileAdd.call(this,name,data,o);}
return this;},folder:function(arg)
{if(!arg)
{throw new Error("folder : wrong argument");}
if(arg instanceof RegExp)
{return this.filter(function(relativePath,file){return file.options.dir&&arg.test(relativePath);});}
var name=this.root+ arg;var newFolder=folderAdd.call(this,name);var ret=this.clone();ret.root=newFolder.name;return ret;},remove:function(name)
{name=this.root+ name;var file=this.files[name];if(!file)
{if(name.slice(-1)!="/")name+="/";file=this.files[name];}
if(file)
{if(!file.options.dir)
{delete this.files[name];}
else
{var kids=this.filter(function(relativePath,file){return file.name.slice(0,name.length)===name;});for(var i=0;i<kids.length;i++)
{delete this.files[kids[i].name];}}}
return this;},generate:function(options)
{options=extend(options||{},{base64:true,compression:"STORE"});var compression=options.compression.toUpperCase();var directory=[],files=[],fileOffset=0;if(!JSZip.compressions[compression]){throw compression+" is not a valid compression method !";}
for(var name in this.files)
{if(!this.files.hasOwnProperty(name)){continue;}
var file=this.files[name];var utfEncodedFileName=this.utf8encode(file.name);var fileRecord="",dirRecord="",data=prepareLocalHeaderData.call(this,file,utfEncodedFileName,compression);fileRecord=JSZip.signature.LOCAL_FILE_HEADER+ data.header+ utfEncodedFileName+ data.compressedData;dirRecord=JSZip.signature.CENTRAL_FILE_HEADER+"\x14\x00"+
data.header+"\x00\x00"+"\x00\x00"+"\x00\x00"+
(this.files[name].dir===true?"\x10\x00\x00\x00":"\x00\x00\x00\x00")+
decToHex(fileOffset,4)+
utfEncodedFileName;fileOffset+=fileRecord.length;files.push(fileRecord);directory.push(dirRecord);}
var fileData=files.join("");var dirData=directory.join("");var dirEnd="";dirEnd=JSZip.signature.CENTRAL_DIRECTORY_END+"\x00\x00"+"\x00\x00"+
decToHex(files.length,2)+
decToHex(files.length,2)+
decToHex(dirData.length,4)+
decToHex(fileData.length,4)+"\x00\x00";var zip=fileData+ dirData+ dirEnd;return(options.base64)?JSZipBase64.encode(zip):zip;},crc32:function(str,crc)
{if(str===""||typeof str==="undefined")return 0;var table="00000000 77073096 EE0E612C 990951BA 076DC419 706AF48F E963A535 9E6495A3 0EDB8832 79DCB8A4 E0D5E91E 97D2D988 09B64C2B 7EB17CBD E7B82D07 90BF1D91 1DB71064 6AB020F2 F3B97148 84BE41DE 1ADAD47D 6DDDE4EB F4D4B551 83D385C7 136C9856 646BA8C0 FD62F97A 8A65C9EC 14015C4F 63066CD9 FA0F3D63 8D080DF5 3B6E20C8 4C69105E D56041E4 A2677172 3C03E4D1 4B04D447 D20D85FD A50AB56B 35B5A8FA 42B2986C DBBBC9D6 ACBCF940 32D86CE3 45DF5C75 DCD60DCF ABD13D59 26D930AC 51DE003A C8D75180 BFD06116 21B4F4B5 56B3C423 CFBA9599 B8BDA50F 2802B89E 5F058808 C60CD9B2 B10BE924 2F6F7C87 58684C11 C1611DAB B6662D3D 76DC4190 01DB7106 98D220BC EFD5102A 71B18589 06B6B51F 9FBFE4A5 E8B8D433 7807C9A2 0F00F934 9609A88E E10E9818 7F6A0DBB 086D3D2D 91646C97 E6635C01 6B6B51F4 1C6C6162 856530D8 F262004E 6C0695ED 1B01A57B 8208F4C1 F50FC457 65B0D9C6 12B7E950 8BBEB8EA FCB9887C 62DD1DDF 15DA2D49 8CD37CF3 FBD44C65 4DB26158 3AB551CE A3BC0074 D4BB30E2 4ADFA541 3DD895D7 A4D1C46D D3D6F4FB 4369E96A 346ED9FC AD678846 DA60B8D0 44042D73 33031DE5 AA0A4C5F DD0D7CC9 5005713C 270241AA BE0B1010 C90C2086 5768B525 206F85B3 B966D409 CE61E49F 5EDEF90E 29D9C998 B0D09822 C7D7A8B4 59B33D17 2EB40D81 B7BD5C3B C0BA6CAD EDB88320 9ABFB3B6 03B6E20C 74B1D29A EAD54739 9DD277AF 04DB2615 73DC1683 E3630B12 94643B84 0D6D6A3E 7A6A5AA8 E40ECF0B 9309FF9D 0A00AE27 7D079EB1 F00F9344 8708A3D2 1E01F268 6906C2FE F762575D 806567CB 196C3671 6E6B06E7 FED41B76 89D32BE0 10DA7A5A 67DD4ACC F9B9DF6F 8EBEEFF9 17B7BE43 60B08ED5 D6D6A3E8 A1D1937E 38D8C2C4 4FDFF252 D1BB67F1 A6BC5767 3FB506DD 48B2364B D80D2BDA AF0A1B4C 36034AF6 41047A60 DF60EFC3 A867DF55 316E8EEF 4669BE79 CB61B38C BC66831A 256FD2A0 5268E236 CC0C7795 BB0B4703 220216B9 5505262F C5BA3BBE B2BD0B28 2BB45A92 5CB36A04 C2D7FFA7 B5D0CF31 2CD99E8B 5BDEAE1D 9B64C2B0 EC63F226 756AA39C 026D930A 9C0906A9 EB0E363F 72076785 05005713 95BF4A82 E2B87A14 7BB12BAE 0CB61B38 92D28E9B E5D5BE0D 7CDCEFB7 0BDBDF21 86D3D2D4 F1D4E242 68DDB3F8 1FDA836E 81BE16CD F6B9265B 6FB077E1 18B74777 88085AE6 FF0F6A70 66063BCA 11010B5C 8F659EFF F862AE69 616BFFD3 166CCF45 A00AE278 D70DD2EE 4E048354 3903B3C2 A7672661 D06016F7 4969474D 3E6E77DB AED16A4A D9D65ADC 40DF0B66 37D83BF0 A9BCAE53 DEBB9EC5 47B2CF7F 30B5FFE9 BDBDF21C CABAC28A 53B39330 24B4A3A6 BAD03605 CDD70693 54DE5729 23D967BF B3667A2E C4614AB8 5D681B02 2A6F2B94 B40BBE37 C30C8EA1 5A05DF1B 2D02EF8D";if(typeof(crc)=="undefined"){crc=0;}
var x=0;var y=0;crc=crc^(-1);for(var i=0,iTop=str.length;i<iTop;i++){y=(crc^str.charCodeAt(i))&0xFF;x="0x"+ table.substr(y*9,8);crc=(crc>>>8)^x;}
return crc^(-1);},clone:function()
{var newObj=new JSZip();for(var i in this)
{if(typeof this[i]!=="function")
{newObj[i]=this[i];}}
return newObj;},utf8encode:function(string){string=string.replace(/\r\n/g,"\n");var utftext="";for(var n=0;n<string.length;n++){var c=string.charCodeAt(n);if(c<128){utftext+=String.fromCharCode(c);}
else if((c>127)&&(c<2048)){utftext+=String.fromCharCode((c>>6)|192);utftext+=String.fromCharCode((c&63)|128);}
else{utftext+=String.fromCharCode((c>>12)|224);utftext+=String.fromCharCode(((c>>6)&63)|128);utftext+=String.fromCharCode((c&63)|128);}}
return utftext;},utf8decode:function(utftext){var string="";var i=0;var c=0,c1=0,c2=0,c3=0;while(i<utftext.length){c=utftext.charCodeAt(i);if(c<128){string+=String.fromCharCode(c);i++;}
else if((c>191)&&(c<224)){c2=utftext.charCodeAt(i+1);string+=String.fromCharCode(((c&31)<<6)|(c2&63));i+=2;}
else{c2=utftext.charCodeAt(i+1);c3=utftext.charCodeAt(i+2);string+=String.fromCharCode(((c&15)<<12)|((c2&63)<<6)|(c3&63));i+=3;}}
return string;}};})();JSZip.compressions={"STORE":{magic:"\x00\x00",compress:function(content)
{return content;},uncompress:function(content)
{return content;}}};var JSZipBase64=function(){var _keyStr="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";return{encode:function(input,utf8){var output="";var chr1,chr2,chr3,enc1,enc2,enc3,enc4;var i=0;while(i<input.length){chr1=input.charCodeAt(i++);chr2=input.charCodeAt(i++);chr3=input.charCodeAt(i++);enc1=chr1>>2;enc2=((chr1&3)<<4)|(chr2>>4);enc3=((chr2&15)<<2)|(chr3>>6);enc4=chr3&63;if(isNaN(chr2)){enc3=enc4=64;}else if(isNaN(chr3)){enc4=64;}
output=output+
_keyStr.charAt(enc1)+ _keyStr.charAt(enc2)+
_keyStr.charAt(enc3)+ _keyStr.charAt(enc4);}
return output;},decode:function(input,utf8){var output="";var chr1,chr2,chr3;var enc1,enc2,enc3,enc4;var i=0;input=input.replace(/[^A-Za-z0-9\+\/\=]/g,"");while(i<input.length){enc1=_keyStr.indexOf(input.charAt(i++));enc2=_keyStr.indexOf(input.charAt(i++));enc3=_keyStr.indexOf(input.charAt(i++));enc4=_keyStr.indexOf(input.charAt(i++));chr1=(enc1<<2)|(enc2>>4);chr2=((enc2&15)<<4)|(enc3>>2);chr3=((enc3&3)<<6)|enc4;output=output+ String.fromCharCode(chr1);if(enc3!=64){output=output+ String.fromCharCode(chr2);}
if(enc4!=64){output=output+ String.fromCharCode(chr3);}}
return output;}};}();


if(!JSZip)
{throw"JSZip not defined";}
(function(){var zip_WSIZE=32768;var zip_STORED_BLOCK=0;var zip_STATIC_TREES=1;var zip_DYN_TREES=2;var zip_DEFAULT_LEVEL=6;var zip_FULL_SEARCH=true;var zip_INBUFSIZ=32768;var zip_INBUF_EXTRA=64;var zip_OUTBUFSIZ=1024*8;var zip_window_size=2*zip_WSIZE;var zip_MIN_MATCH=3;var zip_MAX_MATCH=258;var zip_BITS=16;var zip_LIT_BUFSIZE=0x2000;var zip_HASH_BITS=13;if(zip_LIT_BUFSIZE>zip_INBUFSIZ)
alert("error: zip_INBUFSIZ is too small");if((zip_WSIZE<<1)>(1<<zip_BITS))
alert("error: zip_WSIZE is too large");if(zip_HASH_BITS>zip_BITS-1)
alert("error: zip_HASH_BITS is too large");if(zip_HASH_BITS<8||zip_MAX_MATCH!=258)
alert("error: Code too clever");var zip_DIST_BUFSIZE=zip_LIT_BUFSIZE;var zip_HASH_SIZE=1<<zip_HASH_BITS;var zip_HASH_MASK=zip_HASH_SIZE- 1;var zip_WMASK=zip_WSIZE- 1;var zip_NIL=0;var zip_TOO_FAR=4096;var zip_MIN_LOOKAHEAD=zip_MAX_MATCH+ zip_MIN_MATCH+ 1;var zip_MAX_DIST=zip_WSIZE- zip_MIN_LOOKAHEAD;var zip_SMALLEST=1;var zip_MAX_BITS=15;var zip_MAX_BL_BITS=7;var zip_LENGTH_CODES=29;var zip_LITERALS=256;var zip_END_BLOCK=256;var zip_L_CODES=zip_LITERALS+ 1+ zip_LENGTH_CODES;var zip_D_CODES=30;var zip_BL_CODES=19;var zip_REP_3_6=16;var zip_REPZ_3_10=17;var zip_REPZ_11_138=18;var zip_HEAP_SIZE=2*zip_L_CODES+ 1;var zip_H_SHIFT=parseInt((zip_HASH_BITS+ zip_MIN_MATCH- 1)/zip_MIN_MATCH);var zip_free_queue;var zip_qhead,zip_qtail;var zip_initflag;var zip_outbuf=null;var zip_outcnt,zip_outoff;var zip_complete;var zip_window;var zip_d_buf;var zip_l_buf;var zip_prev;var zip_bi_buf;var zip_bi_valid;var zip_block_start;var zip_ins_h;var zip_hash_head;var zip_prev_match;var zip_match_available;var zip_match_length;var zip_prev_length;var zip_strstart;var zip_match_start;var zip_eofile;var zip_lookahead;var zip_max_chain_length;var zip_max_lazy_match;var zip_compr_level;var zip_good_match;var zip_nice_match;var zip_dyn_ltree;var zip_dyn_dtree;var zip_static_ltree;var zip_static_dtree;var zip_bl_tree;var zip_l_desc;var zip_d_desc;var zip_bl_desc;var zip_bl_count;var zip_heap;var zip_heap_len;var zip_heap_max;var zip_depth;var zip_length_code;var zip_dist_code;var zip_base_length;var zip_base_dist;var zip_flag_buf;var zip_last_lit;var zip_last_dist;var zip_last_flags;var zip_flags;var zip_flag_bit;var zip_opt_len;var zip_static_len;var zip_deflate_data;var zip_deflate_pos;var zip_DeflateCT=function(){this.fc=0;this.dl=0;}
var zip_DeflateTreeDesc=function(){this.dyn_tree=null;this.static_tree=null;this.extra_bits=null;this.extra_base=0;this.elems=0;this.max_length=0;this.max_code=0;}
var zip_DeflateConfiguration=function(a,b,c,d){this.good_length=a;this.max_lazy=b;this.nice_length=c;this.max_chain=d;}
var zip_DeflateBuffer=function(){this.next=null;this.len=0;this.ptr=new Array(zip_OUTBUFSIZ);this.off=0;}
var zip_extra_lbits=new Array(0,0,0,0,0,0,0,0,1,1,1,1,2,2,2,2,3,3,3,3,4,4,4,4,5,5,5,5,0);var zip_extra_dbits=new Array(0,0,0,0,1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9,10,10,11,11,12,12,13,13);var zip_extra_blbits=new Array(0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,3,7);var zip_bl_order=new Array(16,17,18,0,8,7,9,6,10,5,11,4,12,3,13,2,14,1,15);var zip_configuration_table=new Array(new zip_DeflateConfiguration(0,0,0,0),new zip_DeflateConfiguration(4,4,8,4),new zip_DeflateConfiguration(4,5,16,8),new zip_DeflateConfiguration(4,6,32,32),new zip_DeflateConfiguration(4,4,16,16),new zip_DeflateConfiguration(8,16,32,32),new zip_DeflateConfiguration(8,16,128,128),new zip_DeflateConfiguration(8,32,128,256),new zip_DeflateConfiguration(32,128,258,1024),new zip_DeflateConfiguration(32,258,258,4096));var zip_deflate_start=function(level){var i;if(!level)
level=zip_DEFAULT_LEVEL;else if(level<1)
level=1;else if(level>9)
level=9;zip_compr_level=level;zip_initflag=false;zip_eofile=false;if(zip_outbuf!=null)
return;zip_free_queue=zip_qhead=zip_qtail=null;zip_outbuf=new Array(zip_OUTBUFSIZ);zip_window=new Array(zip_window_size);zip_d_buf=new Array(zip_DIST_BUFSIZE);zip_l_buf=new Array(zip_INBUFSIZ+ zip_INBUF_EXTRA);zip_prev=new Array(1<<zip_BITS);zip_dyn_ltree=new Array(zip_HEAP_SIZE);for(i=0;i<zip_HEAP_SIZE;i++)
zip_dyn_ltree[i]=new zip_DeflateCT();zip_dyn_dtree=new Array(2*zip_D_CODES+1);for(i=0;i<2*zip_D_CODES+1;i++)
zip_dyn_dtree[i]=new zip_DeflateCT();zip_static_ltree=new Array(zip_L_CODES+2);for(i=0;i<zip_L_CODES+2;i++)
zip_static_ltree[i]=new zip_DeflateCT();zip_static_dtree=new Array(zip_D_CODES);for(i=0;i<zip_D_CODES;i++)
zip_static_dtree[i]=new zip_DeflateCT();zip_bl_tree=new Array(2*zip_BL_CODES+1);for(i=0;i<2*zip_BL_CODES+1;i++)
zip_bl_tree[i]=new zip_DeflateCT();zip_l_desc=new zip_DeflateTreeDesc();zip_d_desc=new zip_DeflateTreeDesc();zip_bl_desc=new zip_DeflateTreeDesc();zip_bl_count=new Array(zip_MAX_BITS+1);zip_heap=new Array(2*zip_L_CODES+1);zip_depth=new Array(2*zip_L_CODES+1);zip_length_code=new Array(zip_MAX_MATCH-zip_MIN_MATCH+1);zip_dist_code=new Array(512);zip_base_length=new Array(zip_LENGTH_CODES);zip_base_dist=new Array(zip_D_CODES);zip_flag_buf=new Array(parseInt(zip_LIT_BUFSIZE/8));}
var zip_deflate_end=function(){zip_free_queue=zip_qhead=zip_qtail=null;zip_outbuf=null;zip_window=null;zip_d_buf=null;zip_l_buf=null;zip_prev=null;zip_dyn_ltree=null;zip_dyn_dtree=null;zip_static_ltree=null;zip_static_dtree=null;zip_bl_tree=null;zip_l_desc=null;zip_d_desc=null;zip_bl_desc=null;zip_bl_count=null;zip_heap=null;zip_depth=null;zip_length_code=null;zip_dist_code=null;zip_base_length=null;zip_base_dist=null;zip_flag_buf=null;}
var zip_reuse_queue=function(p){p.next=zip_free_queue;zip_free_queue=p;}
var zip_new_queue=function(){var p;if(zip_free_queue!=null)
{p=zip_free_queue;zip_free_queue=zip_free_queue.next;}
else
p=new zip_DeflateBuffer();p.next=null;p.len=p.off=0;return p;}
var zip_head1=function(i){return zip_prev[zip_WSIZE+ i];}
var zip_head2=function(i,val){return zip_prev[zip_WSIZE+ i]=val;}
var zip_put_byte=function(c){zip_outbuf[zip_outoff+ zip_outcnt++]=c;if(zip_outoff+ zip_outcnt==zip_OUTBUFSIZ)
zip_qoutbuf();}
var zip_put_short=function(w){w&=0xffff;if(zip_outoff+ zip_outcnt<zip_OUTBUFSIZ- 2){zip_outbuf[zip_outoff+ zip_outcnt++]=(w&0xff);zip_outbuf[zip_outoff+ zip_outcnt++]=(w>>>8);}else{zip_put_byte(w&0xff);zip_put_byte(w>>>8);}}
var zip_INSERT_STRING=function(){zip_ins_h=((zip_ins_h<<zip_H_SHIFT)^(zip_window[zip_strstart+ zip_MIN_MATCH- 1]&0xff))&zip_HASH_MASK;zip_hash_head=zip_head1(zip_ins_h);zip_prev[zip_strstart&zip_WMASK]=zip_hash_head;zip_head2(zip_ins_h,zip_strstart);}
var zip_SEND_CODE=function(c,tree){zip_send_bits(tree[c].fc,tree[c].dl);}
var zip_D_CODE=function(dist){return(dist<256?zip_dist_code[dist]:zip_dist_code[256+(dist>>7)])&0xff;}
var zip_SMALLER=function(tree,n,m){return tree[n].fc<tree[m].fc||(tree[n].fc==tree[m].fc&&zip_depth[n]<=zip_depth[m]);}
var zip_read_buff=function(buff,offset,n){var i;for(i=0;i<n&&zip_deflate_pos<zip_deflate_data.length;i++)
buff[offset+ i]=zip_deflate_data.charCodeAt(zip_deflate_pos++)&0xff;return i;}
var zip_lm_init=function(){var j;for(j=0;j<zip_HASH_SIZE;j++)
zip_prev[zip_WSIZE+ j]=0;zip_max_lazy_match=zip_configuration_table[zip_compr_level].max_lazy;zip_good_match=zip_configuration_table[zip_compr_level].good_length;if(!zip_FULL_SEARCH)
zip_nice_match=zip_configuration_table[zip_compr_level].nice_length;zip_max_chain_length=zip_configuration_table[zip_compr_level].max_chain;zip_strstart=0;zip_block_start=0;zip_lookahead=zip_read_buff(zip_window,0,2*zip_WSIZE);if(zip_lookahead<=0){zip_eofile=true;zip_lookahead=0;return;}
zip_eofile=false;while(zip_lookahead<zip_MIN_LOOKAHEAD&&!zip_eofile)
zip_fill_window();zip_ins_h=0;for(j=0;j<zip_MIN_MATCH- 1;j++){zip_ins_h=((zip_ins_h<<zip_H_SHIFT)^(zip_window[j]&0xff))&zip_HASH_MASK;}}
var zip_longest_match=function(cur_match){var chain_length=zip_max_chain_length;var scanp=zip_strstart;var matchp;var len;var best_len=zip_prev_length;var limit=(zip_strstart>zip_MAX_DIST?zip_strstart- zip_MAX_DIST:zip_NIL);var strendp=zip_strstart+ zip_MAX_MATCH;var scan_end1=zip_window[scanp+ best_len- 1];var scan_end=zip_window[scanp+ best_len];if(zip_prev_length>=zip_good_match)
chain_length>>=2;do{matchp=cur_match;if(zip_window[matchp+ best_len]!=scan_end||zip_window[matchp+ best_len- 1]!=scan_end1||zip_window[matchp]!=zip_window[scanp]||zip_window[++matchp]!=zip_window[scanp+ 1]){continue;}
scanp+=2;matchp++;do{}while(zip_window[++scanp]==zip_window[++matchp]&&zip_window[++scanp]==zip_window[++matchp]&&zip_window[++scanp]==zip_window[++matchp]&&zip_window[++scanp]==zip_window[++matchp]&&zip_window[++scanp]==zip_window[++matchp]&&zip_window[++scanp]==zip_window[++matchp]&&zip_window[++scanp]==zip_window[++matchp]&&zip_window[++scanp]==zip_window[++matchp]&&scanp<strendp);len=zip_MAX_MATCH-(strendp- scanp);scanp=strendp- zip_MAX_MATCH;if(len>best_len){zip_match_start=cur_match;best_len=len;if(zip_FULL_SEARCH){if(len>=zip_MAX_MATCH)break;}else{if(len>=zip_nice_match)break;}
scan_end1=zip_window[scanp+ best_len-1];scan_end=zip_window[scanp+ best_len];}}while((cur_match=zip_prev[cur_match&zip_WMASK])>limit&&--chain_length!=0);return best_len;}
var zip_fill_window=function(){var n,m;var more=zip_window_size- zip_lookahead- zip_strstart;if(more==-1){more--;}else if(zip_strstart>=zip_WSIZE+ zip_MAX_DIST){for(n=0;n<zip_WSIZE;n++)
zip_window[n]=zip_window[n+ zip_WSIZE];zip_match_start-=zip_WSIZE;zip_strstart-=zip_WSIZE;zip_block_start-=zip_WSIZE;for(n=0;n<zip_HASH_SIZE;n++){m=zip_head1(n);zip_head2(n,m>=zip_WSIZE?m- zip_WSIZE:zip_NIL);}
for(n=0;n<zip_WSIZE;n++){m=zip_prev[n];zip_prev[n]=(m>=zip_WSIZE?m- zip_WSIZE:zip_NIL);}
more+=zip_WSIZE;}
if(!zip_eofile){n=zip_read_buff(zip_window,zip_strstart+ zip_lookahead,more);if(n<=0)
zip_eofile=true;else
zip_lookahead+=n;}}
var zip_deflate_fast=function(){while(zip_lookahead!=0&&zip_qhead==null){var flush;zip_INSERT_STRING();if(zip_hash_head!=zip_NIL&&zip_strstart- zip_hash_head<=zip_MAX_DIST){zip_match_length=zip_longest_match(zip_hash_head);if(zip_match_length>zip_lookahead)
zip_match_length=zip_lookahead;}
if(zip_match_length>=zip_MIN_MATCH){flush=zip_ct_tally(zip_strstart- zip_match_start,zip_match_length- zip_MIN_MATCH);zip_lookahead-=zip_match_length;if(zip_match_length<=zip_max_lazy_match){zip_match_length--;do{zip_strstart++;zip_INSERT_STRING();}while(--zip_match_length!=0);zip_strstart++;}else{zip_strstart+=zip_match_length;zip_match_length=0;zip_ins_h=zip_window[zip_strstart]&0xff;zip_ins_h=((zip_ins_h<<zip_H_SHIFT)^(zip_window[zip_strstart+ 1]&0xff))&zip_HASH_MASK;}}else{flush=zip_ct_tally(0,zip_window[zip_strstart]&0xff);zip_lookahead--;zip_strstart++;}
if(flush){zip_flush_block(0);zip_block_start=zip_strstart;}
while(zip_lookahead<zip_MIN_LOOKAHEAD&&!zip_eofile)
zip_fill_window();}}
var zip_deflate_better=function(){while(zip_lookahead!=0&&zip_qhead==null){zip_INSERT_STRING();zip_prev_length=zip_match_length;zip_prev_match=zip_match_start;zip_match_length=zip_MIN_MATCH- 1;if(zip_hash_head!=zip_NIL&&zip_prev_length<zip_max_lazy_match&&zip_strstart- zip_hash_head<=zip_MAX_DIST){zip_match_length=zip_longest_match(zip_hash_head);if(zip_match_length>zip_lookahead)
zip_match_length=zip_lookahead;if(zip_match_length==zip_MIN_MATCH&&zip_strstart- zip_match_start>zip_TOO_FAR){zip_match_length--;}}
if(zip_prev_length>=zip_MIN_MATCH&&zip_match_length<=zip_prev_length){var flush;flush=zip_ct_tally(zip_strstart- 1- zip_prev_match,zip_prev_length- zip_MIN_MATCH);zip_lookahead-=zip_prev_length- 1;zip_prev_length-=2;do{zip_strstart++;zip_INSERT_STRING();}while(--zip_prev_length!=0);zip_match_available=0;zip_match_length=zip_MIN_MATCH- 1;zip_strstart++;if(flush){zip_flush_block(0);zip_block_start=zip_strstart;}}else if(zip_match_available!=0){if(zip_ct_tally(0,zip_window[zip_strstart- 1]&0xff)){zip_flush_block(0);zip_block_start=zip_strstart;}
zip_strstart++;zip_lookahead--;}else{zip_match_available=1;zip_strstart++;zip_lookahead--;}
while(zip_lookahead<zip_MIN_LOOKAHEAD&&!zip_eofile)
zip_fill_window();}}
var zip_init_deflate=function(){if(zip_eofile)
return;zip_bi_buf=0;zip_bi_valid=0;zip_ct_init();zip_lm_init();zip_qhead=null;zip_outcnt=0;zip_outoff=0;if(zip_compr_level<=3)
{zip_prev_length=zip_MIN_MATCH- 1;zip_match_length=0;}
else
{zip_match_length=zip_MIN_MATCH- 1;zip_match_available=0;}
zip_complete=false;}
var zip_deflate_internal=function(buff,off,buff_size){var n;if(!zip_initflag)
{zip_init_deflate();zip_initflag=true;if(zip_lookahead==0){zip_complete=true;return 0;}}
if((n=zip_qcopy(buff,off,buff_size))==buff_size)
return buff_size;if(zip_complete)
return n;if(zip_compr_level<=3)
zip_deflate_fast();else
zip_deflate_better();if(zip_lookahead==0){if(zip_match_available!=0)
zip_ct_tally(0,zip_window[zip_strstart- 1]&0xff);zip_flush_block(1);zip_complete=true;}
return n+ zip_qcopy(buff,n+ off,buff_size- n);}
var zip_qcopy=function(buff,off,buff_size){var n,i,j;n=0;while(zip_qhead!=null&&n<buff_size)
{i=buff_size- n;if(i>zip_qhead.len)
i=zip_qhead.len;for(j=0;j<i;j++)
buff[off+ n+ j]=zip_qhead.ptr[zip_qhead.off+ j];zip_qhead.off+=i;zip_qhead.len-=i;n+=i;if(zip_qhead.len==0){var p;p=zip_qhead;zip_qhead=zip_qhead.next;zip_reuse_queue(p);}}
if(n==buff_size)
return n;if(zip_outoff<zip_outcnt){i=buff_size- n;if(i>zip_outcnt- zip_outoff)
i=zip_outcnt- zip_outoff;for(j=0;j<i;j++)
buff[off+ n+ j]=zip_outbuf[zip_outoff+ j];zip_outoff+=i;n+=i;if(zip_outcnt==zip_outoff)
zip_outcnt=zip_outoff=0;}
return n;}
var zip_ct_init=function(){var n;var bits;var length;var code;var dist;if(zip_static_dtree[0].dl!=0)return;zip_l_desc.dyn_tree=zip_dyn_ltree;zip_l_desc.static_tree=zip_static_ltree;zip_l_desc.extra_bits=zip_extra_lbits;zip_l_desc.extra_base=zip_LITERALS+ 1;zip_l_desc.elems=zip_L_CODES;zip_l_desc.max_length=zip_MAX_BITS;zip_l_desc.max_code=0;zip_d_desc.dyn_tree=zip_dyn_dtree;zip_d_desc.static_tree=zip_static_dtree;zip_d_desc.extra_bits=zip_extra_dbits;zip_d_desc.extra_base=0;zip_d_desc.elems=zip_D_CODES;zip_d_desc.max_length=zip_MAX_BITS;zip_d_desc.max_code=0;zip_bl_desc.dyn_tree=zip_bl_tree;zip_bl_desc.static_tree=null;zip_bl_desc.extra_bits=zip_extra_blbits;zip_bl_desc.extra_base=0;zip_bl_desc.elems=zip_BL_CODES;zip_bl_desc.max_length=zip_MAX_BL_BITS;zip_bl_desc.max_code=0;length=0;for(code=0;code<zip_LENGTH_CODES-1;code++){zip_base_length[code]=length;for(n=0;n<(1<<zip_extra_lbits[code]);n++)
zip_length_code[length++]=code;}
zip_length_code[length-1]=code;dist=0;for(code=0;code<16;code++){zip_base_dist[code]=dist;for(n=0;n<(1<<zip_extra_dbits[code]);n++){zip_dist_code[dist++]=code;}}
dist>>=7;for(;code<zip_D_CODES;code++){zip_base_dist[code]=dist<<7;for(n=0;n<(1<<(zip_extra_dbits[code]-7));n++)
zip_dist_code[256+ dist++]=code;}
for(bits=0;bits<=zip_MAX_BITS;bits++)
zip_bl_count[bits]=0;n=0;while(n<=143){zip_static_ltree[n++].dl=8;zip_bl_count[8]++;}
while(n<=255){zip_static_ltree[n++].dl=9;zip_bl_count[9]++;}
while(n<=279){zip_static_ltree[n++].dl=7;zip_bl_count[7]++;}
while(n<=287){zip_static_ltree[n++].dl=8;zip_bl_count[8]++;}
zip_gen_codes(zip_static_ltree,zip_L_CODES+ 1);for(n=0;n<zip_D_CODES;n++){zip_static_dtree[n].dl=5;zip_static_dtree[n].fc=zip_bi_reverse(n,5);}
zip_init_block();}
var zip_init_block=function(){var n;for(n=0;n<zip_L_CODES;n++)zip_dyn_ltree[n].fc=0;for(n=0;n<zip_D_CODES;n++)zip_dyn_dtree[n].fc=0;for(n=0;n<zip_BL_CODES;n++)zip_bl_tree[n].fc=0;zip_dyn_ltree[zip_END_BLOCK].fc=1;zip_opt_len=zip_static_len=0;zip_last_lit=zip_last_dist=zip_last_flags=0;zip_flags=0;zip_flag_bit=1;}
var zip_pqdownheap=function(tree,k){var v=zip_heap[k];var j=k<<1;while(j<=zip_heap_len){if(j<zip_heap_len&&zip_SMALLER(tree,zip_heap[j+ 1],zip_heap[j]))
j++;if(zip_SMALLER(tree,v,zip_heap[j]))
break;zip_heap[k]=zip_heap[j];k=j;j<<=1;}
zip_heap[k]=v;}
var zip_gen_bitlen=function(desc){var tree=desc.dyn_tree;var extra=desc.extra_bits;var base=desc.extra_base;var max_code=desc.max_code;var max_length=desc.max_length;var stree=desc.static_tree;var h;var n,m;var bits;var xbits;var f;var overflow=0;for(bits=0;bits<=zip_MAX_BITS;bits++)
zip_bl_count[bits]=0;tree[zip_heap[zip_heap_max]].dl=0;for(h=zip_heap_max+ 1;h<zip_HEAP_SIZE;h++){n=zip_heap[h];bits=tree[tree[n].dl].dl+ 1;if(bits>max_length){bits=max_length;overflow++;}
tree[n].dl=bits;if(n>max_code)
continue;zip_bl_count[bits]++;xbits=0;if(n>=base)
xbits=extra[n- base];f=tree[n].fc;zip_opt_len+=f*(bits+ xbits);if(stree!=null)
zip_static_len+=f*(stree[n].dl+ xbits);}
if(overflow==0)
return;do{bits=max_length- 1;while(zip_bl_count[bits]==0)
bits--;zip_bl_count[bits]--;zip_bl_count[bits+ 1]+=2;zip_bl_count[max_length]--;overflow-=2;}while(overflow>0);for(bits=max_length;bits!=0;bits--){n=zip_bl_count[bits];while(n!=0){m=zip_heap[--h];if(m>max_code)
continue;if(tree[m].dl!=bits){zip_opt_len+=(bits- tree[m].dl)*tree[m].fc;tree[m].fc=bits;}
n--;}}}
var zip_gen_codes=function(tree,max_code){var next_code=new Array(zip_MAX_BITS+1);var code=0;var bits;var n;for(bits=1;bits<=zip_MAX_BITS;bits++){code=((code+ zip_bl_count[bits-1])<<1);next_code[bits]=code;}
for(n=0;n<=max_code;n++){var len=tree[n].dl;if(len==0)
continue;tree[n].fc=zip_bi_reverse(next_code[len]++,len);}}
var zip_build_tree=function(desc){var tree=desc.dyn_tree;var stree=desc.static_tree;var elems=desc.elems;var n,m;var max_code=-1;var node=elems;zip_heap_len=0;zip_heap_max=zip_HEAP_SIZE;for(n=0;n<elems;n++){if(tree[n].fc!=0){zip_heap[++zip_heap_len]=max_code=n;zip_depth[n]=0;}else
tree[n].dl=0;}
while(zip_heap_len<2){var xnew=zip_heap[++zip_heap_len]=(max_code<2?++max_code:0);tree[xnew].fc=1;zip_depth[xnew]=0;zip_opt_len--;if(stree!=null)
zip_static_len-=stree[xnew].dl;}
desc.max_code=max_code;for(n=zip_heap_len>>1;n>=1;n--)
zip_pqdownheap(tree,n);do{n=zip_heap[zip_SMALLEST];zip_heap[zip_SMALLEST]=zip_heap[zip_heap_len--];zip_pqdownheap(tree,zip_SMALLEST);m=zip_heap[zip_SMALLEST];zip_heap[--zip_heap_max]=n;zip_heap[--zip_heap_max]=m;tree[node].fc=tree[n].fc+ tree[m].fc;if(zip_depth[n]>zip_depth[m]+ 1)
zip_depth[node]=zip_depth[n];else
zip_depth[node]=zip_depth[m]+ 1;tree[n].dl=tree[m].dl=node;zip_heap[zip_SMALLEST]=node++;zip_pqdownheap(tree,zip_SMALLEST);}while(zip_heap_len>=2);zip_heap[--zip_heap_max]=zip_heap[zip_SMALLEST];zip_gen_bitlen(desc);zip_gen_codes(tree,max_code);}
var zip_scan_tree=function(tree,max_code){var n;var prevlen=-1;var curlen;var nextlen=tree[0].dl;var count=0;var max_count=7;var min_count=4;if(nextlen==0){max_count=138;min_count=3;}
tree[max_code+ 1].dl=0xffff;for(n=0;n<=max_code;n++){curlen=nextlen;nextlen=tree[n+ 1].dl;if(++count<max_count&&curlen==nextlen)
continue;else if(count<min_count)
zip_bl_tree[curlen].fc+=count;else if(curlen!=0){if(curlen!=prevlen)
zip_bl_tree[curlen].fc++;zip_bl_tree[zip_REP_3_6].fc++;}else if(count<=10)
zip_bl_tree[zip_REPZ_3_10].fc++;else
zip_bl_tree[zip_REPZ_11_138].fc++;count=0;prevlen=curlen;if(nextlen==0){max_count=138;min_count=3;}else if(curlen==nextlen){max_count=6;min_count=3;}else{max_count=7;min_count=4;}}}
var zip_send_tree=function(tree,max_code){var n;var prevlen=-1;var curlen;var nextlen=tree[0].dl;var count=0;var max_count=7;var min_count=4;if(nextlen==0){max_count=138;min_count=3;}
for(n=0;n<=max_code;n++){curlen=nextlen;nextlen=tree[n+1].dl;if(++count<max_count&&curlen==nextlen){continue;}else if(count<min_count){do{zip_SEND_CODE(curlen,zip_bl_tree);}while(--count!=0);}else if(curlen!=0){if(curlen!=prevlen){zip_SEND_CODE(curlen,zip_bl_tree);count--;}
zip_SEND_CODE(zip_REP_3_6,zip_bl_tree);zip_send_bits(count- 3,2);}else if(count<=10){zip_SEND_CODE(zip_REPZ_3_10,zip_bl_tree);zip_send_bits(count-3,3);}else{zip_SEND_CODE(zip_REPZ_11_138,zip_bl_tree);zip_send_bits(count-11,7);}
count=0;prevlen=curlen;if(nextlen==0){max_count=138;min_count=3;}else if(curlen==nextlen){max_count=6;min_count=3;}else{max_count=7;min_count=4;}}}
var zip_build_bl_tree=function(){var max_blindex;zip_scan_tree(zip_dyn_ltree,zip_l_desc.max_code);zip_scan_tree(zip_dyn_dtree,zip_d_desc.max_code);zip_build_tree(zip_bl_desc);for(max_blindex=zip_BL_CODES-1;max_blindex>=3;max_blindex--){if(zip_bl_tree[zip_bl_order[max_blindex]].dl!=0)break;}
zip_opt_len+=3*(max_blindex+1)+ 5+5+4;return max_blindex;}
var zip_send_all_trees=function(lcodes,dcodes,blcodes){var rank;zip_send_bits(lcodes-257,5);zip_send_bits(dcodes-1,5);zip_send_bits(blcodes-4,4);for(rank=0;rank<blcodes;rank++){zip_send_bits(zip_bl_tree[zip_bl_order[rank]].dl,3);}
zip_send_tree(zip_dyn_ltree,lcodes-1);zip_send_tree(zip_dyn_dtree,dcodes-1);}
var zip_flush_block=function(eof){var opt_lenb,static_lenb;var max_blindex;var stored_len;stored_len=zip_strstart- zip_block_start;zip_flag_buf[zip_last_flags]=zip_flags;zip_build_tree(zip_l_desc);zip_build_tree(zip_d_desc);max_blindex=zip_build_bl_tree();opt_lenb=(zip_opt_len+3+7)>>3;static_lenb=(zip_static_len+3+7)>>3;if(static_lenb<=opt_lenb)
opt_lenb=static_lenb;if(stored_len+ 4<=opt_lenb&&zip_block_start>=0){var i;zip_send_bits((zip_STORED_BLOCK<<1)+eof,3);zip_bi_windup();zip_put_short(stored_len);zip_put_short(~stored_len);for(i=0;i<stored_len;i++)
zip_put_byte(zip_window[zip_block_start+ i]);}else if(static_lenb==opt_lenb){zip_send_bits((zip_STATIC_TREES<<1)+eof,3);zip_compress_block(zip_static_ltree,zip_static_dtree);}else{zip_send_bits((zip_DYN_TREES<<1)+eof,3);zip_send_all_trees(zip_l_desc.max_code+1,zip_d_desc.max_code+1,max_blindex+1);zip_compress_block(zip_dyn_ltree,zip_dyn_dtree);}
zip_init_block();if(eof!=0)
zip_bi_windup();}
var zip_ct_tally=function(dist,lc){zip_l_buf[zip_last_lit++]=lc;if(dist==0){zip_dyn_ltree[lc].fc++;}else{dist--;zip_dyn_ltree[zip_length_code[lc]+zip_LITERALS+1].fc++;zip_dyn_dtree[zip_D_CODE(dist)].fc++;zip_d_buf[zip_last_dist++]=dist;zip_flags|=zip_flag_bit;}
zip_flag_bit<<=1;if((zip_last_lit&7)==0){zip_flag_buf[zip_last_flags++]=zip_flags;zip_flags=0;zip_flag_bit=1;}
if(zip_compr_level>2&&(zip_last_lit&0xfff)==0){var out_length=zip_last_lit*8;var in_length=zip_strstart- zip_block_start;var dcode;for(dcode=0;dcode<zip_D_CODES;dcode++){out_length+=zip_dyn_dtree[dcode].fc*(5+ zip_extra_dbits[dcode]);}
out_length>>=3;if(zip_last_dist<parseInt(zip_last_lit/2)&&out_length<parseInt(in_length/2))
return true;}
return(zip_last_lit==zip_LIT_BUFSIZE-1||zip_last_dist==zip_DIST_BUFSIZE);}
var zip_compress_block=function(ltree,dtree){var dist;var lc;var lx=0;var dx=0;var fx=0;var flag=0;var code;var extra;if(zip_last_lit!=0)do{if((lx&7)==0)
flag=zip_flag_buf[fx++];lc=zip_l_buf[lx++]&0xff;if((flag&1)==0){zip_SEND_CODE(lc,ltree);}else{code=zip_length_code[lc];zip_SEND_CODE(code+zip_LITERALS+1,ltree);extra=zip_extra_lbits[code];if(extra!=0){lc-=zip_base_length[code];zip_send_bits(lc,extra);}
dist=zip_d_buf[dx++];code=zip_D_CODE(dist);zip_SEND_CODE(code,dtree);extra=zip_extra_dbits[code];if(extra!=0){dist-=zip_base_dist[code];zip_send_bits(dist,extra);}}
flag>>=1;}while(lx<zip_last_lit);zip_SEND_CODE(zip_END_BLOCK,ltree);}
var zip_Buf_size=16;var zip_send_bits=function(value,length){if(zip_bi_valid>zip_Buf_size- length){zip_bi_buf|=(value<<zip_bi_valid);zip_put_short(zip_bi_buf);zip_bi_buf=(value>>(zip_Buf_size- zip_bi_valid));zip_bi_valid+=length- zip_Buf_size;}else{zip_bi_buf|=value<<zip_bi_valid;zip_bi_valid+=length;}}
var zip_bi_reverse=function(code,len){var res=0;do{res|=code&1;code>>=1;res<<=1;}while(--len>0);return res>>1;}
var zip_bi_windup=function(){if(zip_bi_valid>8){zip_put_short(zip_bi_buf);}else if(zip_bi_valid>0){zip_put_byte(zip_bi_buf);}
zip_bi_buf=0;zip_bi_valid=0;}
var zip_qoutbuf=function(){if(zip_outcnt!=0){var q,i;q=zip_new_queue();if(zip_qhead==null)
zip_qhead=zip_qtail=q;else
zip_qtail=zip_qtail.next=q;q.len=zip_outcnt- zip_outoff;for(i=0;i<q.len;i++)
q.ptr[i]=zip_outbuf[zip_outoff+ i];zip_outcnt=zip_outoff=0;}}
var zip_deflate=function(str,level){var i,j;zip_deflate_data=str;zip_deflate_pos=0;if(typeof level=="undefined")
level=zip_DEFAULT_LEVEL;zip_deflate_start(level);var buff=new Array(1024);var aout=[];while((i=zip_deflate_internal(buff,0,buff.length))>0){var cbuf=new Array(i);for(j=0;j<i;j++){cbuf[j]=String.fromCharCode(buff[j]);}
aout[aout.length]=cbuf.join("");}
zip_deflate_data=null;return aout.join("");}
if(!JSZip.compressions["DEFLATE"]){JSZip.compressions["DEFLATE"]={magic:"\x08\x00",compress:zip_deflate}}else{JSZip.compressions["DEFLATE"].compress=zip_deflate;}})();


if(!JSZip)
{throw"JSZip not defined";}
(function(){var zip_fixed_bd;var zip_WSIZE=32768;var zip_STORED_BLOCK=0;var zip_STATIC_TREES=1;var zip_DYN_TREES=2;var zip_lbits=9;var zip_dbits=6;var zip_INBUFSIZ=32768;var zip_INBUF_EXTRA=64;var zip_slide;var zip_wp;var zip_fixed_tl=null;var zip_fixed_td;var zip_fixed_bl,fixed_bd;var zip_bit_buf;var zip_bit_len;var zip_method;var zip_eof;var zip_copy_leng;var zip_copy_dist;var zip_tl,zip_td;var zip_bl,zip_bd;var zip_inflate_data;var zip_inflate_pos;var zip_MASK_BITS=new Array(0x0000,0x0001,0x0003,0x0007,0x000f,0x001f,0x003f,0x007f,0x00ff,0x01ff,0x03ff,0x07ff,0x0fff,0x1fff,0x3fff,0x7fff,0xffff);var zip_cplens=new Array(3,4,5,6,7,8,9,10,11,13,15,17,19,23,27,31,35,43,51,59,67,83,99,115,131,163,195,227,258,0,0);var zip_cplext=new Array(0,0,0,0,0,0,0,0,1,1,1,1,2,2,2,2,3,3,3,3,4,4,4,4,5,5,5,5,0,99,99);var zip_cpdist=new Array(1,2,3,4,5,7,9,13,17,25,33,49,65,97,129,193,257,385,513,769,1025,1537,2049,3073,4097,6145,8193,12289,16385,24577);var zip_cpdext=new Array(0,0,0,0,1,1,2,2,3,3,4,4,5,5,6,6,7,7,8,8,9,9,10,10,11,11,12,12,13,13);var zip_border=new Array(16,17,18,0,8,7,9,6,10,5,11,4,12,3,13,2,14,1,15);function zip_HuftList(){this.next=null;this.list=null;}
function zip_HuftNode(){this.e=0;this.b=0;this.n=0;this.t=null;}
function zip_HuftBuild(b,n,s,d,e,mm){this.BMAX=16;this.N_MAX=288;this.status=0;this.root=null;this.m=0;{var a;var c=new Array(this.BMAX+1);var el;var f;var g;var h;var i;var j;var k;var lx=new Array(this.BMAX+1);var p;var pidx;var q;var r=new zip_HuftNode();var u=new Array(this.BMAX);var v=new Array(this.N_MAX);var w;var x=new Array(this.BMAX+1);var xp;var y;var z;var o;var tail;tail=this.root=null;for(i=0;i<c.length;i++)
c[i]=0;for(i=0;i<lx.length;i++)
lx[i]=0;for(i=0;i<u.length;i++)
u[i]=null;for(i=0;i<v.length;i++)
v[i]=0;for(i=0;i<x.length;i++)
x[i]=0;el=n>256?b[256]:this.BMAX;p=b;pidx=0;i=n;do{c[p[pidx]]++;pidx++;}while(--i>0);if(c[0]==n){this.root=null;this.m=0;this.status=0;return;}
for(j=1;j<=this.BMAX;j++)
if(c[j]!=0)
break;k=j;if(mm<j)
mm=j;for(i=this.BMAX;i!=0;i--)
if(c[i]!=0)
break;g=i;if(mm>i)
mm=i;for(y=1<<j;j<i;j++,y<<=1)
if((y-=c[j])<0){this.status=2;this.m=mm;return;}
if((y-=c[i])<0){this.status=2;this.m=mm;return;}
c[i]+=y;x[1]=j=0;p=c;pidx=1;xp=2;while(--i>0)
x[xp++]=(j+=p[pidx++]);p=b;pidx=0;i=0;do{if((j=p[pidx++])!=0)
v[x[j]++]=i;}while(++i<n);n=x[g];x[0]=i=0;p=v;pidx=0;h=-1;w=lx[0]=0;q=null;z=0;for(;k<=g;k++){a=c[k];while(a-->0){while(k>w+ lx[1+ h]){w+=lx[1+ h];h++;z=(z=g- w)>mm?mm:z;if((f=1<<(j=k- w))>a+ 1){f-=a+ 1;xp=k;while(++j<z){if((f<<=1)<=c[++xp])
break;f-=c[xp];}}
if(w+ j>el&&w<el)
j=el- w;z=1<<j;lx[1+ h]=j;q=new Array(z);for(o=0;o<z;o++){q[o]=new zip_HuftNode();}
if(tail==null)
tail=this.root=new zip_HuftList();else
tail=tail.next=new zip_HuftList();tail.next=null;tail.list=q;u[h]=q;if(h>0){x[h]=i;r.b=lx[h];r.e=16+ j;r.t=q;j=(i&((1<<w)- 1))>>(w- lx[h]);u[h-1][j].e=r.e;u[h-1][j].b=r.b;u[h-1][j].n=r.n;u[h-1][j].t=r.t;}}
r.b=k- w;if(pidx>=n)
r.e=99;else if(p[pidx]<s){r.e=(p[pidx]<256?16:15);r.n=p[pidx++];}else{r.e=e[p[pidx]- s];r.n=d[p[pidx++]- s];}
f=1<<(k- w);for(j=i>>w;j<z;j+=f){q[j].e=r.e;q[j].b=r.b;q[j].n=r.n;q[j].t=r.t;}
for(j=1<<(k- 1);(i&j)!=0;j>>=1)
i^=j;i^=j;while((i&((1<<w)- 1))!=x[h]){w-=lx[h];h--;}}}
this.m=lx[1];this.status=((y!=0&&g!=1)?1:0);}}
function zip_GET_BYTE(){if(zip_inflate_data.length==zip_inflate_pos)
return-1;return zip_inflate_data.charCodeAt(zip_inflate_pos++)&0xff;}
function zip_NEEDBITS(n){while(zip_bit_len<n){zip_bit_buf|=zip_GET_BYTE()<<zip_bit_len;zip_bit_len+=8;}}
function zip_GETBITS(n){return zip_bit_buf&zip_MASK_BITS[n];}
function zip_DUMPBITS(n){zip_bit_buf>>=n;zip_bit_len-=n;}
function zip_inflate_codes(buff,off,size){var e;var t;var n;if(size==0)
return 0;n=0;for(;;){zip_NEEDBITS(zip_bl);t=zip_tl.list[zip_GETBITS(zip_bl)];e=t.e;while(e>16){if(e==99)
return-1;zip_DUMPBITS(t.b);e-=16;zip_NEEDBITS(e);t=t.t[zip_GETBITS(e)];e=t.e;}
zip_DUMPBITS(t.b);if(e==16){zip_wp&=zip_WSIZE- 1;buff[off+ n++]=zip_slide[zip_wp++]=t.n;if(n==size)
return size;continue;}
if(e==15)
break;zip_NEEDBITS(e);zip_copy_leng=t.n+ zip_GETBITS(e);zip_DUMPBITS(e);zip_NEEDBITS(zip_bd);t=zip_td.list[zip_GETBITS(zip_bd)];e=t.e;while(e>16){if(e==99)
return-1;zip_DUMPBITS(t.b);e-=16;zip_NEEDBITS(e);t=t.t[zip_GETBITS(e)];e=t.e;}
zip_DUMPBITS(t.b);zip_NEEDBITS(e);zip_copy_dist=zip_wp- t.n- zip_GETBITS(e);zip_DUMPBITS(e);while(zip_copy_leng>0&&n<size){zip_copy_leng--;zip_copy_dist&=zip_WSIZE- 1;zip_wp&=zip_WSIZE- 1;buff[off+ n++]=zip_slide[zip_wp++]=zip_slide[zip_copy_dist++];}
if(n==size)
return size;}
zip_method=-1;return n;}
function zip_inflate_stored(buff,off,size){var n;n=zip_bit_len&7;zip_DUMPBITS(n);zip_NEEDBITS(16);n=zip_GETBITS(16);zip_DUMPBITS(16);zip_NEEDBITS(16);if(n!=((~zip_bit_buf)&0xffff))
return-1;zip_DUMPBITS(16);zip_copy_leng=n;n=0;while(zip_copy_leng>0&&n<size){zip_copy_leng--;zip_wp&=zip_WSIZE- 1;zip_NEEDBITS(8);buff[off+ n++]=zip_slide[zip_wp++]=zip_GETBITS(8);zip_DUMPBITS(8);}
if(zip_copy_leng==0)
zip_method=-1;return n;}
function zip_inflate_fixed(buff,off,size){if(zip_fixed_tl==null){var i;var l=new Array(288);var h;for(i=0;i<144;i++)
l[i]=8;for(;i<256;i++)
l[i]=9;for(;i<280;i++)
l[i]=7;for(;i<288;i++)
l[i]=8;zip_fixed_bl=7;h=new zip_HuftBuild(l,288,257,zip_cplens,zip_cplext,zip_fixed_bl);if(h.status!=0){alert("HufBuild error: "+h.status);return-1;}
zip_fixed_tl=h.root;zip_fixed_bl=h.m;for(i=0;i<30;i++)
l[i]=5;zip_fixed_bd=5;h=new zip_HuftBuild(l,30,0,zip_cpdist,zip_cpdext,zip_fixed_bd);if(h.status>1){zip_fixed_tl=null;alert("HufBuild error: "+h.status);return-1;}
zip_fixed_td=h.root;zip_fixed_bd=h.m;}
zip_tl=zip_fixed_tl;zip_td=zip_fixed_td;zip_bl=zip_fixed_bl;zip_bd=zip_fixed_bd;return zip_inflate_codes(buff,off,size);}
function zip_inflate_dynamic(buff,off,size){var i;var j;var l;var n;var t;var nb;var nl;var nd;var ll=new Array(286+30);var h;for(i=0;i<ll.length;i++)
ll[i]=0;zip_NEEDBITS(5);nl=257+ zip_GETBITS(5);zip_DUMPBITS(5);zip_NEEDBITS(5);nd=1+ zip_GETBITS(5);zip_DUMPBITS(5);zip_NEEDBITS(4);nb=4+ zip_GETBITS(4);zip_DUMPBITS(4);if(nl>286||nd>30)
return-1;for(j=0;j<nb;j++)
{zip_NEEDBITS(3);ll[zip_border[j]]=zip_GETBITS(3);zip_DUMPBITS(3);}
for(;j<19;j++)
ll[zip_border[j]]=0;zip_bl=7;h=new zip_HuftBuild(ll,19,19,null,null,zip_bl);if(h.status!=0)
return-1;zip_tl=h.root;zip_bl=h.m;n=nl+ nd;i=l=0;while(i<n){zip_NEEDBITS(zip_bl);t=zip_tl.list[zip_GETBITS(zip_bl)];j=t.b;zip_DUMPBITS(j);j=t.n;if(j<16)
ll[i++]=l=j;else if(j==16){zip_NEEDBITS(2);j=3+ zip_GETBITS(2);zip_DUMPBITS(2);if(i+ j>n)
return-1;while(j-->0)
ll[i++]=l;}else if(j==17){zip_NEEDBITS(3);j=3+ zip_GETBITS(3);zip_DUMPBITS(3);if(i+ j>n)
return-1;while(j-->0)
ll[i++]=0;l=0;}else{zip_NEEDBITS(7);j=11+ zip_GETBITS(7);zip_DUMPBITS(7);if(i+ j>n)
return-1;while(j-->0)
ll[i++]=0;l=0;}}
zip_bl=zip_lbits;h=new zip_HuftBuild(ll,nl,257,zip_cplens,zip_cplext,zip_bl);if(zip_bl==0)
h.status=1;if(h.status!=0){/*if(h.status==1);*/return-1;}
zip_tl=h.root;zip_bl=h.m;for(i=0;i<nd;i++)
ll[i]=ll[i+ nl];zip_bd=zip_dbits;h=new zip_HuftBuild(ll,nd,0,zip_cpdist,zip_cpdext,zip_bd);zip_td=h.root;zip_bd=h.m;if(zip_bd==0&&nl>257){return-1;}
//if(h.status==1){;}
if(h.status!=0)
return-1;return zip_inflate_codes(buff,off,size);}
function zip_inflate_start(){var i;if(zip_slide==null)
zip_slide=new Array(2*zip_WSIZE);zip_wp=0;zip_bit_buf=0;zip_bit_len=0;zip_method=-1;zip_eof=false;zip_copy_leng=zip_copy_dist=0;zip_tl=null;}
function zip_inflate_internal(buff,off,size){var n,i;n=0;while(n<size){if(zip_eof&&zip_method==-1)
return n;if(zip_copy_leng>0){if(zip_method!=zip_STORED_BLOCK){while(zip_copy_leng>0&&n<size){zip_copy_leng--;zip_copy_dist&=zip_WSIZE- 1;zip_wp&=zip_WSIZE- 1;buff[off+ n++]=zip_slide[zip_wp++]=zip_slide[zip_copy_dist++];}}else{while(zip_copy_leng>0&&n<size){zip_copy_leng--;zip_wp&=zip_WSIZE- 1;zip_NEEDBITS(8);buff[off+ n++]=zip_slide[zip_wp++]=zip_GETBITS(8);zip_DUMPBITS(8);}
if(zip_copy_leng==0)
zip_method=-1;}
if(n==size)
return n;}
if(zip_method==-1){if(zip_eof)
break;zip_NEEDBITS(1);if(zip_GETBITS(1)!=0)
zip_eof=true;zip_DUMPBITS(1);zip_NEEDBITS(2);zip_method=zip_GETBITS(2);zip_DUMPBITS(2);zip_tl=null;zip_copy_leng=0;}
switch(zip_method){case 0:i=zip_inflate_stored(buff,off+ n,size- n);break;case 1:if(zip_tl!=null)
i=zip_inflate_codes(buff,off+ n,size- n);else
i=zip_inflate_fixed(buff,off+ n,size- n);break;case 2:if(zip_tl!=null)
i=zip_inflate_codes(buff,off+ n,size- n);else
i=zip_inflate_dynamic(buff,off+ n,size- n);break;default:i=-1;break;}
if(i==-1){if(zip_eof)
return 0;return-1;}
n+=i;}
return n;}
function zip_inflate(str){var out,buff;var i,j;zip_inflate_start();zip_inflate_data=str;zip_inflate_pos=0;buff=new Array(1024);out="";while((i=zip_inflate_internal(buff,0,buff.length))>0){for(j=0;j<i;j++)
out+=String.fromCharCode(buff[j]);}
zip_inflate_data=null;return out;}
if(!JSZip.compressions["DEFLATE"]){JSZip.compressions["DEFLATE"]={magic:"\x08\x00",uncompress:zip_inflate}}else{JSZip.compressions["DEFLATE"].uncompress=zip_inflate;}})();



(function(){var pretty=function(str)
{var res='',code,i;for(i=0;i<str.length;i++)
{code=str.charCodeAt(i);res+='\\x'+(code<10?"0":"")+ code.toString(16);}
return res;};var findCompression=function(compressionMethod)
{for(var method in JSZip.compressions)
{if(JSZip.compressions[method].magic===compressionMethod)
{return JSZip.compressions[method];}}
return null;};function StreamReader(stream){this.stream=stream;this.index=0;}
StreamReader.prototype={checkOffset:function(offset)
{this.checkIndex(this.index+ offset);},checkIndex:function(newIndex)
{if(this.stream.length<newIndex||newIndex<0)
{throw new Error("End of stream reached (stream length = "+
this.stream.length+", asked index = "+
(newIndex)+"). Corrupted zip ?");}},setIndex:function(newIndex)
{this.checkIndex(newIndex);this.index=newIndex;},eof:function()
{return this.index>=this.stream.length;},byteAt:function(i)
{return this.stream.charCodeAt(i)&0xff;},readByte:function()
{this.checkOffset(1);return this.byteAt(1+ this.index++);},readInt:function(size)
{var result=0,i;this.checkOffset(size);for(i=size- 1;i>=0;i--)
{result=(result<<8)+ this.byteAt(this.index+ i);}
this.index+=size;return result;},readString:function(size)
{var result="",i,code;this.checkOffset(size);for(i=0;i<size;i++)
{code=this.byteAt(this.index+ i);result+=String.fromCharCode(code);}
this.index+=size;return result;},readDate:function()
{var dostime=this.readInt(4);return new Date(((dostime>>25)&0x7f)+ 1980,((dostime>>21)&0x0f)- 1,(dostime>>16)&0x1f,(dostime>>11)&0x1f,(dostime>>5)&0x3f,(dostime&0x1f)<<1);}};function ZipEntry(options,loadOptions)
{this.options=options;this.loadOptions=loadOptions;}
ZipEntry.prototype={isEncrypted:function()
{return(this.bitFlag&0x0001)===0x0001;},hasDataDescriptor:function()
{return(this.bitFlag&0x0008)===0x0008;},useUTF8:function()
{return(this.bitFlag&0x0800)===0x0800;},isZIP64:function()
{return this.options.zip64;},readLocalPartHeader:function(reader)
{this.versionNeeded=reader.readInt(2);this.bitFlag=reader.readInt(2);this.compressionMethod=reader.readString(2);this.date=reader.readDate();this.crc32=reader.readInt(4);this.compressedSize=reader.readInt(4);this.uncompressedSize=reader.readInt(4);this.fileNameLength=reader.readInt(2);this.extraFieldsLength=reader.readInt(2);if(this.isEncrypted())
{throw new Error("Encrypted zip are not supported");}},readLocalPart:function(reader)
{var compression;this.readLocalPartHeader(reader);this.fileName=reader.readString(this.fileNameLength);this.readExtraFields(reader);if(!this.hasDataDescriptor())
{this.compressedFileData=reader.readString(this.compressedSize);}
else
{this.compressedFileData=this.findDataUntilDataDescriptor(reader);this.crc32=reader.readInt(4);this.compressedSize=reader.readInt(this.isZIP64()?8:4);this.uncompressedSize=reader.readInt(this.isZIP64()?8:4);if(this.compressedFileData.length!==this.compressedSize)
{throw new Error("Bug : data descriptor incorrectly read (size mismatch)");}}
this.uncompressedFileData=null;compression=findCompression(this.compressionMethod);if(compression===null)
{throw new Error("Corrupted zip : compression "+ pretty(this.compressionMethod)+" unknown (inner file : "+ this.fileName+")");}
this.uncompressedFileData=compression.uncompress(this.compressedFileData);if(this.loadOptions.checkCRC32&&JSZip.prototype.crc32(this.uncompressedFileData)!==this.crc32)
{throw new Error("Corrupted zip : CRC32 mismatch");}
if(this.useUTF8())
{this.fileName=JSZip.prototype.utf8decode(this.fileName);}},findDataUntilDataDescriptor:function(reader)
{var data="",buffer=reader.readString(4),aByte;while(buffer!==JSZip.signature.DATA_DESCRIPTOR)
{aByte=reader.readString(1);data+=buffer.slice(0,1);buffer=(buffer+ aByte).slice(-4);}
return data;},readCentralPart:function(reader)
{this.versionMadeBy=reader.readString(2);this.readLocalPartHeader(reader);this.fileCommentLength=reader.readInt(2);this.diskNumberStart=reader.readInt(2);this.internalFileAttributes=reader.readInt(2);this.externalFileAttributes=reader.readInt(4);this.localHeaderOffset=reader.readInt(4);this.fileName=reader.readString(this.fileNameLength);this.readExtraFields(reader);this.fileComment=reader.readString(this.fileCommentLength);if(this.useUTF8())
{this.fileName=JSZip.prototype.utf8decode(this.fileName);this.fileComment=JSZip.prototype.utf8decode(this.fileComment);}
this.dir=this.externalFileAttributes&0x00000010?true:false;},parseZIP64ExtraField:function(reader)
{var extraReader=new StreamReader(this.extraFields[0x0001].value);if(this.uncompressedSize===-1)
{this.uncompressedSize=extraReader.readInt(8);}
if(this.compressedSize===-1)
{this.compressedSize=extraReader.readInt(8);}
if(this.localHeaderOffset===-1)
{this.localHeaderOffset=extraReader.readInt(8);}
if(this.diskNumberStart===-1)
{this.diskNumberStart=extraReader.readInt(4);}},readExtraFields:function(reader)
{var start=reader.index,extraFieldId,extraFieldLength,extraFieldValue;this.extraFields=this.extraFields||{};while(reader.index<start+ this.extraFieldsLength)
{extraFieldId=reader.readInt(2);extraFieldLength=reader.readInt(2);extraFieldValue=reader.readString(extraFieldLength);this.extraFields[extraFieldId]={id:extraFieldId,length:extraFieldLength,value:extraFieldValue};}
if(this.isZIP64()&&this.extraFields[0x0001])
{this.parseZIP64ExtraField(reader);}}};function ZipEntries(data,loadOptions)
{this.files=[];this.loadOptions=loadOptions;if(data)this.load(data);}
ZipEntries.prototype={checkSignature:function(expectedSignature)
{var signature=this.reader.readString(4);if(signature!==expectedSignature)
{throw new Error("Corrupted zip or bug : unexpected signature "+"("+ pretty(signature)+", expected "+ pretty(expectedSignature)+")");}},readBlockEndOfCentral:function()
{this.diskNumber=this.reader.readInt(2);this.diskWithCentralDirStart=this.reader.readInt(2);this.centralDirRecordsOnThisDisk=this.reader.readInt(2);this.centralDirRecords=this.reader.readInt(2);this.centralDirSize=this.reader.readInt(4);this.centralDirOffset=this.reader.readInt(4);this.zipCommentLength=this.reader.readInt(2);this.zipComment=this.reader.readString(this.zipCommentLength);},readBlockZip64EndOfCentral:function()
{this.zip64EndOfCentralSize=this.reader.readInt(8);this.versionMadeBy=this.reader.readString(2);this.versionNeeded=this.reader.readInt(2);this.diskNumber=this.reader.readInt(4);this.diskWithCentralDirStart=this.reader.readInt(4);this.centralDirRecordsOnThisDisk=this.reader.readInt(8);this.centralDirRecords=this.reader.readInt(8);this.centralDirSize=this.reader.readInt(8);this.centralDirOffset=this.reader.readInt(8);this.zip64ExtensibleData={};var extraDataSize=this.zip64EndOfCentralSize- 44,index=0,extraFieldId,extraFieldLength,extraFieldValue;while(index<extraDataSize)
{extraFieldId=this.reader.readInt(2);extraFieldLength=this.reader.readInt(4);extraFieldValue=this.reader.readString(extraFieldLength);this.zip64ExtensibleData[extraFieldId]={id:extraFieldId,length:extraFieldLength,value:extraFieldValue};}},readBlockZip64EndOfCentralLocator:function()
{this.diskWithZip64CentralDirStart=this.reader.readInt(4);this.relativeOffsetEndOfZip64CentralDir=this.reader.readInt(8);this.disksCount=this.reader.readInt(4);if(this.disksCount>1)
{throw new Error("Multi-volumes zip are not supported");}},readLocalFiles:function()
{var i,file;for(i=0;i<this.files.length;i++)
{file=this.files[i];this.reader.setIndex(file.localHeaderOffset);this.checkSignature(JSZip.signature.LOCAL_FILE_HEADER);file.readLocalPart(this.reader);}},readCentralDir:function()
{var file;this.reader.setIndex(this.centralDirOffset);while(this.reader.readString(4)===JSZip.signature.CENTRAL_FILE_HEADER)
{file=new ZipEntry({zip64:this.zip64},this.loadOptions);file.readCentralPart(this.reader);this.files.push(file);}},readEndOfCentral:function()
{var offset=this.reader.stream.lastIndexOf(JSZip.signature.ZIP64_CENTRAL_DIRECTORY_LOCATOR);if(offset===-1)
{this.zip64=false;offset=this.reader.stream.lastIndexOf(JSZip.signature.CENTRAL_DIRECTORY_END);if(offset===-1)
{throw new Error("Corrupted zip : can't find end of central directory");}
this.reader.setIndex(offset);this.checkSignature(JSZip.signature.CENTRAL_DIRECTORY_END);this.readBlockEndOfCentral();}
else
{this.zip64=true;this.reader.setIndex(offset);this.checkSignature(JSZip.signature.ZIP64_CENTRAL_DIRECTORY_LOCATOR);this.readBlockZip64EndOfCentralLocator();this.reader.setIndex(this.relativeOffsetEndOfZip64CentralDir);this.checkSignature(JSZip.signature.ZIP64_CENTRAL_DIRECTORY_END);this.readBlockZip64EndOfCentral();}},load:function(data)
{this.reader=new StreamReader(data);this.readEndOfCentral();this.readCentralDir();this.readLocalFiles();}};JSZip.prototype.load=function(data,options)
{var files,zipEntries,i,input;options=options||{};if(options.base64)
{data=JSZipBase64.decode(data);}
zipEntries=new ZipEntries(data,options);files=zipEntries.files;for(i in files)
{input=files[i];this.file(input.fileName,input.uncompressedFileData,{binary:true,date:input.date,dir:input.dir});}
return this;};})();


/*
var JSZip = null
if (typeof require === 'function') {
	JSZip = require('node-zip');
}*/

//----------------------------------------------------------
// Copyright (C) Microsoft Corporation. All rights reserved.
// Released under the Microsoft Office Extensible File License
// https://raw.github.com/stephen-hardy/xlsx.js/master/LICENSE.txt
//----------------------------------------------------------
function xlsx(file) { 
	'use strict'; // v2.3.0

	var defaultFontName = 'Calibri';
	var defaultFontSize = 11;
	var defaultFontColor = "000000";

	var result, zip = new JSZip(), zipTime, processTime, s, f, i, j, k, l, t, w, sharedStrings, styles, index, data, val, style, borders, border, borderIndex, fonts, font, fontIndex,
		docProps, xl, xlWorksheets, worksheet, contentTypes = [[], []], props = [], xlRels = [], worksheets = [], id, columns, cols, colWidth, cell, row, merges, merged,
		numFmts = ['General', '0', '0.00', '#,##0', '#,##0.00',,,,, '0%', '0.00%', '0.00E+00', '# ?/?', '# ??/??', 'mm-dd-yy', 'd-mmm-yy', 'd-mmm', 'mmm-yy', 'h:mm AM/PM', 'h:mm:ss AM/PM',
			'h:mm', 'h:mm:ss', 'm/d/yy h:mm',,,,,,,,,,,,,,, '#,##0 ;(#,##0)', '#,##0 ;[Red](#,##0)', '#,##0.00;(#,##0.00)', '#,##0.00;[Red](#,##0.00)',,,,, 'mm:ss', '[h]:mm:ss', 'mmss.0', '##0.0E+0', '@'],
		alphabet = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';

	function numAlpha(i) { 
		var t = Math.floor(i / 26) - 1; return (t > -1 ? numAlpha(t) : '') + alphabet.charAt(i % 26); 
	}

	function alphaNum(s) { 
		var t = 0; if (s.length === 2) { t = alphaNum(s.charAt(0)) + 1; } return t * 26 + alphabet.indexOf(s.substr(-1)); 
	}

	function convertDate(input) { 
		return typeof input === 'object' ? ((input - new Date(1900, 0, 0)) / 86400000) + 1 : new Date(+new Date(1900, 0, 0) + (input - 1) * 86400000); 
	}

	function typeOf(obj) {
		return ({}).toString.call(obj).match(/\s([a-zA-Z]+)/)[1].toLowerCase();
	}
	
	function getAttr(s, n) { 
		s = s.substr(s.indexOf(n + '="') + n.length + 2); return s.substring(0, s.indexOf('"')); 
	}
	
	function escapeXML(s) { 
		return (s || '').replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;').replace(/'/g, '&#x27;'); 
	} // see http://www.w3.org/TR/xml/#syntax
	
	function unescapeXML(s) { 
		return (s || '').replace(/&amp;/g, '&').replace(/&lt;/g, '<').replace(/&gt;/g, '>').replace(/&quot;/g, '"').replace(/&#x27;/g, '\''); 
	}

   if (file.pivot) { 
   		processTime = Date.now();
   		
   		//file _rels/.rels
   		zip.folder('_rels').file('.rels', '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>'
				+'<Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">'
  					+'<Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument" Target="xl/workbook.xml"/>'
  					+'<Relationship Id="rId2" Type="http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties" Target="docProps/core.xml"/>'
  					+'<Relationship Id="rId3" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties" Target="docProps/app.xml"/>'
				+'</Relationships>');
		
		//files folder docProps
		docProps = zip.folder('docProps');
		docProps.file('core.xml', '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>'
						+'<cp:coreProperties xmlns:cp="http://schemas.openxmlformats.org/package/2006/metadata/core-properties" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:dcterms="http://purl.org/dc/terms/" xmlns:dcmitype="http://purl.org/dc/dcmitype/" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">'
  						+'<dc:creator>gonzalo</dc:creator>'
  						+'<dcterms:created xsi:type="dcterms:W3CDTF">2010-12-23T01:32:21Z</dcterms:created>'
  						+'<cp:lastModifiedBy>xperTools</cp:lastModifiedBy>'
  						+'<dcterms:modified xsi:type="dcterms:W3CDTF">2010-02-23T01:32:21Z</dcterms:modified>'
						+'</cp:coreProperties>')
		docProps.file('app.xml','<?xml version="1.0" encoding="UTF-8" standalone="yes"?>'
						+'<Properties xmlns="http://schemas.openxmlformats.org/officeDocument/2006/extended-properties" xmlns:vt="http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes">'
  							+'<DocSecurity>0</DocSecurity>'
  							+'<ScaleCrop>false</ScaleCrop>'
  								+'<HeadingPairs>'
    								+'<vt:vector size="2" baseType="variant">'
      								+'<vt:variant>'
        								+'<vt:lpstr>Hojas de cálculo</vt:lpstr>'
      								+'</vt:variant>'
      								+'<vt:variant>'
        							+'<vt:i4>2</vt:i4>'
      								+'</vt:variant>'
    								+'</vt:vector>'
  								+'</HeadingPairs>'
  							+'<TitlesOfParts>'
    							+'<vt:vector size="2" baseType="lpstr">'
      								+'<vt:lpstr>PivotTable</vt:lpstr>'
      								+'<vt:lpstr>SourceData</vt:lpstr>'
    							+'</vt:vector>'
  							+'</TitlesOfParts>'
  						+'<LinksUpToDate>false</LinksUpToDate>'
  						+'<SharedDoc>false</SharedDoc>'
  						+'<HyperlinksChanged>false</HyperlinksChanged>'
  						+'<AppVersion>12.0000</AppVersion>'
						+'</Properties>');
						
										
		// [Content_Types].xml
		zip.file('[Content_Types].xml', '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>'
			+ '<Types xmlns="http://schemas.openxmlformats.org/package/2006/content-types">'
			+ '<Default Extension="xml" ContentType="application/xml"/>'
			+ '<Default Extension="rels" ContentType="application/vnd.openxmlformats-package.relationships+xml"/>'
			+ '<Override PartName="/docProps/app.xml" ContentType="application/vnd.openxmlformats-officedocument.extended-properties+xml"/>'
			+ '<Override PartName="/docProps/core.xml" ContentType="application/vnd.openxmlformats-package.core-properties+xml"/>'
			+ '<Override PartName="/xl/workbook.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml"/>'
			+ '<Override PartName="/xl/worksheets/sheet2.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml"/>'
			+ '<Override PartName="/xl/sharedStrings.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.sharedStrings+xml"/>'
			+ '<Override PartName="/xl/styles.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml"/>'
			+ '<Override PartName="/xl/theme/theme1.xml" ContentType="application/vnd.openxmlformats-officedocument.theme+xml"/>'
			+ '<Override PartName="/xl/worksheets/sheet1.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml"/>'
			+ '<Override PartName="/xl/pivotTables/pivotTable1.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.pivotTable+xml"/>'
			+ '<Override PartName="/xl/pivotCache/pivotCacheDefinition1.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.pivotCacheDefinition+xml"/>'
			+ '<Override PartName="/xl/pivotCache/pivotCacheRecords1.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.pivotCacheRecords+xml"/>'
			+ '</Types>');
	
		
		//xl folder	
		xl = zip.folder('xl');		
		
		// xl/_rels/workbook.xml.rels
		xl.folder('_rels').file('workbook.xml.rels', '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>'
			+'<Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">'
  			+'<Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet" Target="worksheets/sheet1.xml"/>'
  			+'<Relationship Id="rId2" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet" Target="worksheets/sheet2.xml"/>'
  			+'<Relationship Id="rId3" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotCacheDefinition" Target="pivotCache/pivotCacheDefinition1.xml"/>'
  			+'<Relationship Id="rId4" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme" Target="theme/theme1.xml"/>'
  			+'<Relationship Id="rId5" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles" Target="styles.xml"/>'
  			+'<Relationship Id="rId6" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/sharedStrings" Target="sharedStrings.xml"/>'
			+'</Relationships>');
		
		//pivot cache folder
		var folderPivotCache = xl.folder('pivotCache').file('pivotCacheRecords1.xml', '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>'
			+'<pivotCacheRecords xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships">'
			+'</pivotCacheRecords>');
			
			
		folderPivotCache.file( 'pivotCacheDefinition1.xml', file.pivotCacheDefinition)
	
		
		folderPivotCache.folder('_rels').file('pivotCacheDefinition1.xml.rels', '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>'
					+'<Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">'
  					+'<Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotCacheRecords" Target="pivotCacheRecords1.xml"/>'
					+'</Relationships>')
		
		
		
		//folder pivot table	
		var folderPivotTable = xl.folder('pivotTables').file('pivotTable1.xml', file.pivotTable1);
		
		folderPivotTable.folder('_rels').file('pivotTable1.xml.rels','<?xml version="1.0" encoding="UTF-8" standalone="yes"?>'
				+'<Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">'
  				+'<Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotCacheDefinition" Target="../pivotCache/pivotCacheDefinition1.xml"/>'
				+'</Relationships>');
		
		//folder theme file theme1.xml
		xl.folder('theme').file('theme1.xml', '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>'
				+'<a:theme name="Tema de Office" xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main"/>');
		
		//xl/worksheets
		xlWorksheets = xl.folder('worksheets');
		xlWorksheets.file('sheet1.xml', '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>'
				+'<worksheet xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships">'
  				+'<sheetData/>'
				+'</worksheet>');
				
				
				
				// Content dependent
    	styles = new Array(1);
    	borders = new Array(1);
    	fonts = new Array(1);
    	var fills = "";
    	var fillNumber = 1;
		w = file.worksheets.length;
		while (w--) { 
			// Generate worksheet (gather sharedStrings), and possibly table files, then generate entries for constant files below
			id = w + 1;
			// Generate sheetX.xml in var s
			worksheet = file.worksheets[w]; data = worksheet.data;
			s = '';
			columns = [];
    		merges = [];
			i = -1; l = data.length;
			while (++i < l) {
				j = -1; k = data[i].length;
				s += '<row r="' + (i + 1) + '" x14ac:dyDescent="0.25">';
				while (++j < k) {
					var isString = false;
					cell = data[i][j]; val = cell.hasOwnProperty('value') ? cell.value : cell; t = ''; 
					// supported styles: borders, hAlign, formatCode and font style
					style = {
						borders: cell.borders, 
						hAlign: cell.hAlign,
						vAlign: cell.vAlign,
						bold: cell.bold,
						italic: cell.italic,
						fontColor: cell.fontColor,
						fontName: cell.fontName,
						fontSize: cell.fontSize,
						fill: cell.fill,
						formatCode: cell.formatCode || 'General'
					};
					colWidth = 0;
					if ((cell.isString) || (val && typeof val === 'string' && !isFinite(val))) { 
						// If value is string, and not string of just a number
            			val = escapeXML(val);
						t = 'inlineStr';
						isString=true;
					} else if (typeof val === 'boolean') { 
						val = (val ? 1 : 0); t = 'b'; 
						colWidth = 1;
					} else if (typeOf(val) === 'date') { 
						val = convertDate(val); 
						style.formatCode = cell.formatCode || 'mm-dd-yy'; 
						colWidth = val.length;
					} else if (typeof val === 'object') {
						// unsupported value
						val = null
					} else {
						// number, or string which is a number 
						colWidth = (''+val).length;
					}
					
					// use stringified version as unic and reproductible style signature
					style = JSON.stringify(style);
					index = styles.indexOf(style);
					if (index < 0) { 
						style = styles.push(style) - 1; 
					} else { 
						style = index; 
					}
					// keeps largest cell in column, and autoWidth flag that may be set on any cell
					if (columns[j] == null) {
						columns[j] = {autoWidth: false, max:0};
					}
					if (cell.autoWidth) {
						columns[j].autoWidth = true
					}
					if (colWidth > columns[j].max) {
						columns[j].max = colWidth;
					}
					// store merges if needed and add missing cells. Cannot have rowSpan AND colSpan
					if (cell.colSpan > 1) {
						// horizontal merge. ex: B12:E12. Add missing cells (with same attribute but value) to current row
						merges.push([numAlpha(j) + (i + 1), numAlpha(j+cell.colSpan-1) + (i + 1)]);
						merged = [j, 0]
						for (var m = 0; m < cell.colSpan-1; m++) {
							merged.push(cell);
						}
						data[i].splice.apply(data[i], merged);
						k += cell.colSpan-1;
					} else if (cell.rowSpan > 1) {
						// vertical merge. ex: B12:B15. Add missing cells (with same attribute but value) to next columns
						for (var m = 1; m < cell.rowSpan; m++) {
							if (data[i+m]) {
								data[i+m].splice(j, 0, cell)
							} else {
								// read the end of data
								cell.rowSpan = m;
								break;
							}
						}
						merges.push([numAlpha(j) + (i + 1), numAlpha(j) + (i + cell.rowSpan)]);
					}
					if (cell.rowSpan > 1 ||cell.colSpan > 1) {
						// deletes value, rowSpan and colSpan from cell to avoid refering it from copied cells
						delete cell.value;
						delete cell.rowSpan;
						delete cell.colSpan;
					}
					s += '<c r="' + numAlpha(j) + (i + 1) + '"' + (style ? ' s="' + style + '"' : '') + (t ? ' t="' + t + '"' : '');
					if (val != null) {
						if (isString){
							s += '>' + (cell.formula ? '<f>' + cell.formula + '</f>' : '') + '<is><t>' + val + '</t></is></c>';
						} else {
							s += '>' + (cell.formula ? '<f>' + cell.formula + '</f>' : '') + '<v>' + val + '</v></c>';
						}
					} else {
						s += '/>';
					}
				}
				s += '</row>';
			}

			cols = []
			for (i = 0; i < columns.length; i++) {
				if (columns[i].autoWidth) {
					cols.push('<col min="', i+1, '" max="', i+1, '" width="', columns[i].max, '" bestFit="1"/>');
				}
			}
			// only add cols definition if not empty
			if (cols.length > 0) {
				cols = ['<cols>'].concat(cols, ['</cols>']).join('');
			}

			s = '<?xml version="1.0" encoding="UTF-8" standalone="yes"?><worksheet xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" mc:Ignorable="x14ac" xmlns:x14ac="http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac">'
				+ '<dimension ref="A1:' + numAlpha(data[0].length - 1) + data.length + '"/><sheetViews><sheetView ' + (w === file.activeWorksheet ? 'tabSelected="1" ' : '')
				+ ' workbookViewId="0"/></sheetViews><sheetFormatPr defaultRowHeight="15" x14ac:dyDescent="0.25"/>'
				+ cols
				+ '<sheetData>'
				+ s 
				+ '</sheetData>';
			if (merges.length > 0) {
				s += '<mergeCells count="' + merges.length + '">';
				for (i = 0; i < merges.length; i++) {
					s += '<mergeCell ref="' + merges[i].join(':') + '"/>';
				}
				s += '</mergeCells>';
			}
			s += '<pageMargins left="0.7" right="0.7" top="0.75" bottom="0.75" header="0.3" footer="0.3"/>';
			if (worksheet.table) { 
				s += '<tableParts count="1"><tablePart r:id="rId1"/></tableParts>'; 
			}
			xlWorksheets.file('sheet2.xml', s + '</worksheet>');
		}		
		//xlWorksheets.file('sheet2.xml', file.Sheet2);
		
		// xl/styles.xml
		i = styles.length; t = [];
		var fillIndex = 2;
		var actualFill = 0;
		while (--i) { 
			// Don't process index 0, already added
			style = JSON.parse(styles[i]);

			// cell formating, refer to it if necessary
			if (style.formatCode !== 'General') {
				index = numFmts.indexOf(style.formatCode);
				if (index < 0) { 
					index = 164 + t.length; 
					t.push('<numFmt formatCode="' + style.formatCode + '" numFmtId="' + index + '"/>'); 
				}
				style.formatCode = index
			} else {
				style.formatCode = 0
			}

			// border declaration: add a new declaration and refer to it in style
			borderIndex = 0
			if (style.borders) {
				border = ['<border>']
				// order is significative
				for (var edge in {left:0, right:0, top:0, bottom:0, diagonal:0}) {
					if (style.borders[edge]) {
						var color = style.borders[edge];
						// add transparency if missing
						if (color.length === 6) {
							color = 'FF'+color;
						}
						border.push('<', edge, ' style="thin">', '<color rgb="', style.borders[edge], '"/></', edge, '>');
					} else {
						border.push('<', edge, '/>');
					}
				}
				border.push('</border>');
				border = border.join('');
				// try to reuse existing border
				borderIndex = borders.indexOf(border);
				if (borderIndex < 0) {
					borderIndex = borders.push(border) - 1;
				}
			}

			// font declaration: add a new declaration and refer to it in style
			fontIndex = 0
			if (style.bold || style.italic || style.fontSize || style.fontName || style.fontColor) {
				font = ['<font>']
				if (style.bold) {
					font.push('<b/>');
				}
				if (style.italic) {
					font.push('<i/>');
				}
				font.push('<sz val="', style.fontSize || defaultFontSize, '"/>');
				var colString = 
				font.push('<color rgb="FF', style.fontColor || defaultFontColor,'"/>');
				font.push('<name val="', style.fontName || defaultFontName, '"/>');
				font.push('<family val="2"/>', '</font>');
				font = font.join('');
				// try to reuse existing font
				fontIndex = fonts.indexOf(font);
				if (fontIndex < 0) {
					fontIndex = fonts.push(font) - 1;
				}
			}
			
			//fill declaration: add a new declaration and refer to it in style
			actualFill = 0;
			if (style.fill){
				fills = fills +'<fill><patternFill patternType="solid"><fgColor rgb="FF' + style.fill + '"/>'
							+'<bgColor indexed="64"/></patternFill></fill>'
				fillNumber++;		
				actualFill = fillNumber;	
			}
			
			// declares style, and refer to optionnal formatCode, font and borders
			styles[i] = ['<xf xfId="0" borderId="', //fillId="0"  
				borderIndex, 
				'" fontId="',
				fontIndex,
				'" numFmtId="',
				style.formatCode,
				'" fillId="',
				actualFill,
				'" ',
				(style.hAlign || style.vAlign? 'applyAlignment="1" ' : ' '),
				(style.formatCode > 0 ? 'applyNumberFormat="1" ' : ' '),
				(borderIndex > 0 ? 'applyBorder="1" ' : ' '),
				(fontIndex > 0 ? 'applyFont="1" ' : ' '),
				(actualFill > 0 ? 'applyFill="1" ' : ' '),
				'>'
			];
			if (style.hAlign || style.vAlign) {
				styles[i].push('<alignment');
				if (style.hAlign) {
					styles[i].push(' horizontal="', style.hAlign, '"');
				}
				if (style.vAlign) {
					styles[i].push(' vertical="', style.vAlign, '"');
				}
				styles[i].push('/>');
			}
			styles[i].push('</xf>');
			styles[i] = styles[i].join('');
		}
		t = t.length ? '<numFmts count="' + t.length + '">' + t.join('') + '</numFmts>' : '';
		
		if (file.style != ""){
			t = file.style //TODO: concat with the previous number format
		}
		var multStyle = '<?xml version="1.0" encoding="UTF-8" standalone="yes"?><styleSheet xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" mc:Ignorable="x14ac" xmlns:x14ac="http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac">'
			+ t + '<fonts count="'+ fonts.length + '" x14ac:knownFonts="1"><font><sz val="' + defaultFontSize + '"/><color theme="1"/><name val="' + defaultFontName + '"/><family val="2"/>'
			+ '<scheme val="minor"/></font>' + fonts.join('') + '</fonts>'
			+ '<fills><fill><patternFill patternType="none"/></fill><fill><patternFill patternType="gray125"/></fill>' + fills + '</fills>'
			+ '<borders count="' + borders.length + '"><border><left/><right/><top/><bottom/><diagonal/></border>'
			+ borders.join('') + '</borders><cellStyleXfs count="1">'
			+ '<xf numFmtId="0" fontId="0" fillId="0" borderId="0"/></cellStyleXfs><cellXfs count="' + styles.length + '"><xf numFmtId="0" fontId="0" fillId="0" borderId="0" xfId="0"/>'
			+ styles.join('') + '</cellXfs><cellStyles count="1"><cellStyle name="Normal" xfId="0" builtinId="0"/></cellStyles>' //<dxfs count="0"/>'
		if (file.dfx  != undefined){
			multStyle = multStyle + '<dxfs>' + file.dfx + '</dxfs>' ;	
		} else {
			multStyle = multStyle + '<dxfs count="0"/>'
		}
		multStyle = multStyle + '<tableStyles count="0" defaultTableStyle="TableStyleMedium2" defaultPivotStyle="PivotStyleLight16"/>'
			+ '<extLst><ext uri="{EB79DEF2-80B8-43e5-95BD-54CBDDF9020C}" xmlns:x14="http://schemas.microsoft.com/office/spreadsheetml/2009/9/main">'
			+ '<x14:slicerStyles defaultSlicerStyle="SlicerStyleLight1"/></ext></extLst></styleSheet>'
		
		xl.file('styles.xml', multStyle) 
		//xl.file('styles.xml', file.style);
		
				xlWorksheets.folder('_rels').file('sheet1.xml.rels', '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>'
+'<Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships"><Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/pivotTable" Target="../pivotTables/pivotTable1.xml"/></Relationships>')
		
		//shared strings
		xl.file('sharedStrings.xml', '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>'
				+'<sst xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main">'
				+'</sst>')
		
		
		//workwood
		xl.file('workbook.xml', '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>'
				+'<workbook xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships">'
  				+'<fileVersion appName="xl" lastEdited="4" lowestEdited="4" rupBuild="4506"/>'
  				+'<workbookPr defaultThemeVersion="124226"/>'
  				+'<bookViews>'
    			+'<workbookView xWindow="360" yWindow="555" windowWidth="21015" windowHeight="9405"/>'
  				+'</bookViews>'
  				+'<sheets>'
    			+'<sheet name="PivotTable" sheetId="1" r:id="rId1"/>'
    			+'<sheet name="SourceData" sheetId="2" r:id="rId2"/>'
  				+'</sheets>'
  				+'<calcPr calcId="125725"/>'
  				+'<pivotCaches>'
    			+'<pivotCache cacheId="5" r:id="rId3"/>'
  				+'</pivotCaches>'
  				+'<fileRecoveryPr repairLoad="1"/>'
				+'</workbook>');
				
		
		//xl styles
		
		
	
		processTime = Date.now() - processTime;
		zipTime = Date.now();
		result = {
			base64: zip.generate({ compression: 'DEFLATE' }), zipTime: Date.now() - zipTime, processTime: processTime,
			href: function() { return 'data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,' + this.base64; }
		};
	} else {
		// Save
		processTime = Date.now();
		sharedStrings = [[], 0];
		// Fully static
		zip.folder('_rels').file('.rels', '<?xml version="1.0" encoding="UTF-8" standalone="yes"?><Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships"><Relationship Id="rId3" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties" Target="docProps/app.xml"/><Relationship Id="rId2" Type="http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties" Target="docProps/core.xml"/><Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument" Target="xl/workbook.xml"/></Relationships>');
		docProps = zip.folder('docProps');

		xl = zip.folder('xl');
		xl.folder('theme').file('theme1.xml', '<?xml version="1.0" encoding="UTF-8" standalone="yes"?><a:theme xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main" name="Office Theme"><a:themeElements><a:clrScheme name="Office"><a:dk1><a:sysClr val="windowText" lastClr="000000"/></a:dk1><a:lt1><a:sysClr val="window" lastClr="FFFFFF"/></a:lt1><a:dk2><a:srgbClr val="1F497D"/></a:dk2><a:lt2><a:srgbClr val="EEECE1"/></a:lt2><a:accent1><a:srgbClr val="4F81BD"/></a:accent1><a:accent2><a:srgbClr val="C0504D"/></a:accent2><a:accent3><a:srgbClr val="9BBB59"/></a:accent3><a:accent4><a:srgbClr val="8064A2"/></a:accent4><a:accent5><a:srgbClr val="4BACC6"/></a:accent5><a:accent6><a:srgbClr val="F79646"/></a:accent6><a:hlink><a:srgbClr val="0000FF"/></a:hlink><a:folHlink><a:srgbClr val="800080"/></a:folHlink></a:clrScheme><a:fontScheme name="Office"><a:majorFont><a:latin typeface="Cambria"/><a:ea typeface=""/><a:cs typeface=""/><a:font script="Jpan" typeface="MS P????"/><a:font script="Hang" typeface="?? ??"/><a:font script="Hans" typeface="??"/><a:font script="Hant" typeface="????"/><a:font script="Arab" typeface="Times New Roman"/><a:font script="Hebr" typeface="Times New Roman"/><a:font script="Thai" typeface="Tahoma"/><a:font script="Ethi" typeface="Nyala"/><a:font script="Beng" typeface="Vrinda"/><a:font script="Gujr" typeface="Shruti"/><a:font script="Khmr" typeface="MoolBoran"/><a:font script="Knda" typeface="Tunga"/><a:font script="Guru" typeface="Raavi"/><a:font script="Cans" typeface="Euphemia"/><a:font script="Cher" typeface="Plantagenet Cherokee"/><a:font script="Yiii" typeface="Microsoft Yi Baiti"/><a:font script="Tibt" typeface="Microsoft Himalaya"/><a:font script="Thaa" typeface="MV Boli"/><a:font script="Deva" typeface="Mangal"/><a:font script="Telu" typeface="Gautami"/><a:font script="Taml" typeface="Latha"/><a:font script="Syrc" typeface="Estrangelo Edessa"/><a:font script="Orya" typeface="Kalinga"/><a:font script="Mlym" typeface="Kartika"/><a:font script="Laoo" typeface="DokChampa"/><a:font script="Sinh" typeface="Iskoola Pota"/><a:font script="Mong" typeface="Mongolian Baiti"/><a:font script="Viet" typeface="Times New Roman"/><a:font script="Uigh" typeface="Microsoft Uighur"/><a:font script="Geor" typeface="Sylfaen"/></a:majorFont><a:minorFont><a:latin typeface="Calibri"/><a:ea typeface=""/><a:cs typeface=""/><a:font script="Jpan" typeface="MS P????"/><a:font script="Hang" typeface="?? ??"/><a:font script="Hans" typeface="??"/><a:font script="Hant" typeface="????"/><a:font script="Arab" typeface="Arial"/><a:font script="Hebr" typeface="Arial"/><a:font script="Thai" typeface="Tahoma"/><a:font script="Ethi" typeface="Nyala"/><a:font script="Beng" typeface="Vrinda"/><a:font script="Gujr" typeface="Shruti"/><a:font script="Khmr" typeface="DaunPenh"/><a:font script="Knda" typeface="Tunga"/><a:font script="Guru" typeface="Raavi"/><a:font script="Cans" typeface="Euphemia"/><a:font script="Cher" typeface="Plantagenet Cherokee"/><a:font script="Yiii" typeface="Microsoft Yi Baiti"/><a:font script="Tibt" typeface="Microsoft Himalaya"/><a:font script="Thaa" typeface="MV Boli"/><a:font script="Deva" typeface="Mangal"/><a:font script="Telu" typeface="Gautami"/><a:font script="Taml" typeface="Latha"/><a:font script="Syrc" typeface="Estrangelo Edessa"/><a:font script="Orya" typeface="Kalinga"/><a:font script="Mlym" typeface="Kartika"/><a:font script="Laoo" typeface="DokChampa"/><a:font script="Sinh" typeface="Iskoola Pota"/><a:font script="Mong" typeface="Mongolian Baiti"/><a:font script="Viet" typeface="Arial"/><a:font script="Uigh" typeface="Microsoft Uighur"/><a:font script="Geor" typeface="Sylfaen"/></a:minorFont></a:fontScheme><a:fmtScheme name="Office"><a:fillStyleLst><a:solidFill><a:schemeClr val="phClr"/></a:solidFill><a:gradFill rotWithShape="1"><a:gsLst><a:gs pos="0"><a:schemeClr val="phClr"><a:tint val="50000"/><a:satMod val="300000"/></a:schemeClr></a:gs><a:gs pos="35000"><a:schemeClr val="phClr"><a:tint val="37000"/><a:satMod val="300000"/></a:schemeClr></a:gs><a:gs pos="100000"><a:schemeClr val="phClr"><a:tint val="15000"/><a:satMod val="350000"/></a:schemeClr></a:gs></a:gsLst><a:lin ang="16200000" scaled="1"/></a:gradFill><a:gradFill rotWithShape="1"><a:gsLst><a:gs pos="0"><a:schemeClr val="phClr"><a:shade val="51000"/><a:satMod val="130000"/></a:schemeClr></a:gs><a:gs pos="80000"><a:schemeClr val="phClr"><a:shade val="93000"/><a:satMod val="130000"/></a:schemeClr></a:gs><a:gs pos="100000"><a:schemeClr val="phClr"><a:shade val="94000"/><a:satMod val="135000"/></a:schemeClr></a:gs></a:gsLst><a:lin ang="16200000" scaled="0"/></a:gradFill></a:fillStyleLst><a:lnStyleLst><a:ln w="9525" cap="flat" cmpd="sng" algn="ctr"><a:solidFill><a:schemeClr val="phClr"><a:shade val="95000"/><a:satMod val="105000"/></a:schemeClr></a:solidFill><a:prstDash val="solid"/></a:ln><a:ln w="25400" cap="flat" cmpd="sng" algn="ctr"><a:solidFill><a:schemeClr val="phClr"/></a:solidFill><a:prstDash val="solid"/></a:ln><a:ln w="38100" cap="flat" cmpd="sng" algn="ctr"><a:solidFill><a:schemeClr val="phClr"/></a:solidFill><a:prstDash val="solid"/></a:ln></a:lnStyleLst><a:effectStyleLst><a:effectStyle><a:effectLst><a:outerShdw blurRad="40000" dist="20000" dir="5400000" rotWithShape="0"><a:srgbClr val="000000"><a:alpha val="38000"/></a:srgbClr></a:outerShdw></a:effectLst></a:effectStyle><a:effectStyle><a:effectLst><a:outerShdw blurRad="40000" dist="23000" dir="5400000" rotWithShape="0"><a:srgbClr val="000000"><a:alpha val="35000"/></a:srgbClr></a:outerShdw></a:effectLst></a:effectStyle><a:effectStyle><a:effectLst><a:outerShdw blurRad="40000" dist="23000" dir="5400000" rotWithShape="0"><a:srgbClr val="000000"><a:alpha val="35000"/></a:srgbClr></a:outerShdw></a:effectLst><a:scene3d><a:camera prst="orthographicFront"><a:rot lat="0" lon="0" rev="0"/></a:camera><a:lightRig rig="threePt" dir="t"><a:rot lat="0" lon="0" rev="1200000"/></a:lightRig></a:scene3d><a:sp3d><a:bevelT w="63500" h="25400"/></a:sp3d></a:effectStyle></a:effectStyleLst><a:bgFillStyleLst><a:solidFill><a:schemeClr val="phClr"/></a:solidFill><a:gradFill rotWithShape="1"><a:gsLst><a:gs pos="0"><a:schemeClr val="phClr"><a:tint val="40000"/><a:satMod val="350000"/></a:schemeClr></a:gs><a:gs pos="40000"><a:schemeClr val="phClr"><a:tint val="45000"/><a:shade val="99000"/><a:satMod val="350000"/></a:schemeClr></a:gs><a:gs pos="100000"><a:schemeClr val="phClr"><a:shade val="20000"/><a:satMod val="255000"/></a:schemeClr></a:gs></a:gsLst><a:path path="circle"><a:fillToRect l="50000" t="-80000" r="50000" b="180000"/></a:path></a:gradFill><a:gradFill rotWithShape="1"><a:gsLst><a:gs pos="0"><a:schemeClr val="phClr"><a:tint val="80000"/><a:satMod val="300000"/></a:schemeClr></a:gs><a:gs pos="100000"><a:schemeClr val="phClr"><a:shade val="30000"/><a:satMod val="200000"/></a:schemeClr></a:gs></a:gsLst><a:path path="circle"><a:fillToRect l="50000" t="50000" r="50000" b="50000"/></a:path></a:gradFill></a:bgFillStyleLst></a:fmtScheme></a:themeElements><a:objectDefaults/><a:extraClrSchemeLst/></a:theme>');
		xlWorksheets = xl.folder('worksheets');

		// Not content dependent
		docProps.file('core.xml', '<?xml version="1.0" encoding="UTF-8" standalone="yes"?><cp:coreProperties xmlns:cp="http://schemas.openxmlformats.org/package/2006/metadata/core-properties" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:dcterms="http://purl.org/dc/terms/" xmlns:dcmitype="http://purl.org/dc/dcmitype/" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"><dc:creator>'
			+ (file.creator || 'XLSX.js') + '</dc:creator><cp:lastModifiedBy>' + (file.lastModifiedBy || 'XLSX.js') + '</cp:lastModifiedBy><dcterms:created xsi:type="dcterms:W3CDTF">'
			+ (file.created || new Date()).toISOString() + '</dcterms:created><dcterms:modified xsi:type="dcterms:W3CDTF">' + (file.modified || new Date()).toISOString() + '</dcterms:modified></cp:coreProperties>');

		// Content dependent
    styles = new Array(1);
    borders = new Array(1);
    fonts = new Array(1);
    var fills = "";
    var fillNumber = 1;
		w = file.worksheets.length;
		while (w--) { 
			// Generate worksheet (gather sharedStrings), and possibly table files, then generate entries for constant files below
			id = w + 1;
			// Generate sheetX.xml in var s
			worksheet = file.worksheets[w]; data = worksheet.data;
			s = '';
			columns = [];
    		merges = [];
			i = -1; l = data.length;
			while (++i < l) {
				j = -1; k = data[i].length;
				s += '<row r="' + (i + 1) + '" x14ac:dyDescent="0.25">';
				while (++j < k) {
					cell = data[i][j]; val = cell.hasOwnProperty('value') ? cell.value : cell; t = ''; 
					// supported styles: borders, hAlign, formatCode and font style
					style = {
						borders: cell.borders, 
						hAlign: cell.hAlign,
						vAlign: cell.vAlign,
						bold: cell.bold,
						italic: cell.italic,
						fontColor: cell.fontColor,
						fontName: cell.fontName,
						fontSize: cell.fontSize,
						fill: cell.fill,
						formatCode: cell.formatCode || 'General'
					};
					colWidth = 0;
					if (val && typeof val === 'string' && !isFinite(val)) { 
						// If value is string, and not string of just a number, place a sharedString reference instead of the value
            			val = escapeXML(val);
						sharedStrings[1]++; // Increment total count, unique count derived from sharedStrings[0].length
						index = sharedStrings[0].indexOf(val);
						colWidth = val.length;
						if (index < 0) {
						 	index = sharedStrings[0].push(val) - 1; 
						}
						val = index;
						t = 's';
					} else if (typeof val === 'boolean') { 
						val = (val ? 1 : 0); t = 'b'; 
						colWidth = 1;
					} else if (typeOf(val) === 'date') { 
						val = convertDate(val); 
						style.formatCode = cell.formatCode || 'mm-dd-yy'; 
						colWidth = val.length;
					} else if (typeof val === 'object') {
						// unsupported value
						val = null
					} else {
						// number, or string which is a number 
						colWidth = (''+val).length;
					}
					
					// use stringified version as unic and reproductible style signature
					style = JSON.stringify(style);
					index = styles.indexOf(style);
					if (index < 0) { 
						style = styles.push(style) - 1; 
					} else { 
						style = index; 
					}
					// keeps largest cell in column, and autoWidth flag that may be set on any cell
					if (columns[j] == null) {
						columns[j] = {autoWidth: false, max:0};
					}
					if (cell.autoWidth) {
						columns[j].autoWidth = true
					}
					if (colWidth > columns[j].max) {
						columns[j].max = colWidth;
					}
					// store merges if needed and add missing cells. Cannot have rowSpan AND colSpan
					if (cell.colSpan > 1) {
						// horizontal merge. ex: B12:E12. Add missing cells (with same attribute but value) to current row
						merges.push([numAlpha(j) + (i + 1), numAlpha(j+cell.colSpan-1) + (i + 1)]);
						merged = [j, 0]
						for (var m = 0; m < cell.colSpan-1; m++) {
							merged.push(cell);
						}
						data[i].splice.apply(data[i], merged);
						k += cell.colSpan-1;
					} else if (cell.rowSpan > 1) {
						// vertical merge. ex: B12:B15. Add missing cells (with same attribute but value) to next columns
						for (var m = 1; m < cell.rowSpan; m++) {
							if (data[i+m]) {
								data[i+m].splice(j, 0, cell)
							} else {
								// readh the end of data
								cell.rowSpan = m;
								break;
							}
						}
						merges.push([numAlpha(j) + (i + 1), numAlpha(j) + (i + cell.rowSpan)]);
					}
					if (cell.rowSpan > 1 ||cell.colSpan > 1) {
						// deletes value, rowSpan and colSpan from cell to avoid refering it from copied cells
						delete cell.value;
						delete cell.rowSpan;
						delete cell.colSpan;
					}
					s += '<c r="' + numAlpha(j) + (i + 1) + '"' + (style ? ' s="' + style + '"' : '') + (t ? ' t="' + t + '"' : '');
					if (val != null) {
						s += '>' + (cell.formula ? '<f>' + cell.formula + '</f>' : '') + '<v>' + val + '</v></c>';
					} else {
						s += '/>';
					}
				}
				s += '</row>';
			}

			cols = []
			for (i = 0; i < columns.length; i++) {
				if (columns[i].autoWidth) {
					cols.push('<col min="', i+1, '" max="', i+1, '" width="', columns[i].max, '" bestFit="1"/>');
				}
			}
			// only add cols definition if not empty
			if (cols.length > 0) {
				cols = ['<cols>'].concat(cols, ['</cols>']).join('');
			}

			s = '<?xml version="1.0" encoding="UTF-8" standalone="yes"?><worksheet xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" mc:Ignorable="x14ac" xmlns:x14ac="http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac">'
				+ '<dimension ref="A1:' + numAlpha(data[0].length - 1) + data.length + '"/><sheetViews><sheetView ' + (w === file.activeWorksheet ? 'tabSelected="1" ' : '')
				+ ' workbookViewId="0"/></sheetViews><sheetFormatPr defaultRowHeight="15" x14ac:dyDescent="0.25"/>'
				+ cols
				+ '<sheetData>'
				+ s 
				+ '</sheetData>';
			if (merges.length > 0) {
				s += '<mergeCells count="' + merges.length + '">';
				for (i = 0; i < merges.length; i++) {
					s += '<mergeCell ref="' + merges[i].join(':') + '"/>';
				}
				s += '</mergeCells>';
			}
			s += '<pageMargins left="0.7" right="0.7" top="0.75" bottom="0.75" header="0.3" footer="0.3"/>';
			if (worksheet.table) { 
				s += '<tableParts count="1"><tablePart r:id="rId1"/></tableParts>'; 
			}
			xlWorksheets.file('sheet' + id + '.xml', s + '</worksheet>');

			if (worksheet.table) {
				i = -1; l = data[0].length; t = numAlpha(data[0].length - 1) + data.length;
				s = '<?xml version="1.0" encoding="UTF-8" standalone="yes"?><table xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" id="' + id
					+ '" name="Table' + id + '" displayName="Table' + id + '" ref="A1:' + t + '" totalsRowShown="0"><autoFilter ref="A1:' + t + '"/><tableColumns count="' + data[0].length + '">';
				while (++i < l) { 
					s += '<tableColumn id="' + (i + 1) + '" name="' + (data[0][i].hasOwnProperty('value') ? data[0][i].value : data[0][i]) + '"/>'; 
				}
				s += '</tableColumns><tableStyleInfo name="TableStyleMedium2" showFirstColumn="0" showLastColumn="0" showRowStripes="1" showColumnStripes="0"/></table>';

				xl.folder('tables').file('table' + id + '.xml', s);
				xlWorksheets.folder('_rels').file('sheet' + id + '.xml.rels', '<?xml version="1.0" encoding="UTF-8" standalone="yes"?><Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships"><Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/table" Target="../tables/table' + id + '.xml"/></Relationships>');
				contentTypes[1].unshift('<Override PartName="/xl/tables/table' + id + '.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.table+xml"/>');
			}

			contentTypes[0].unshift('<Override PartName="/xl/worksheets/sheet' + id + '.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml"/>');
			props.unshift(escapeXML(worksheet.name) || 'Sheet' + id);
			xlRels.unshift('<Relationship Id="rId' + id + '" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet" Target="worksheets/sheet' + id + '.xml"/>');
			worksheets.unshift('<sheet name="' + (escapeXML(worksheet.name) || 'Sheet' + id) + '" sheetId="' + id + '" r:id="rId' + id + '"/>');
		}

		// xl/styles.xml
		i = styles.length; t = [];
		var fillIndex = 2;
		var actualFill = 0;
		while (--i) { 
			// Don't process index 0, already added
			style = JSON.parse(styles[i]);

			// cell formating, refer to it if necessary
			if (style.formatCode !== 'General') {
				index = numFmts.indexOf(style.formatCode);
				if (index < 0) { 
					index = 164 + t.length; 
					t.push('<numFmt formatCode="' + style.formatCode + '" numFmtId="' + index + '"/>'); 
				}
				style.formatCode = index
			} else {
				style.formatCode = 0
			}

			// border declaration: add a new declaration and refer to it in style
			borderIndex = 0
			if (style.borders) {
				border = ['<border>']
				// order is significative
				for (var edge in {left:0, right:0, top:0, bottom:0, diagonal:0}) {
					if (style.borders[edge]) {
						var color = style.borders[edge];
						// add transparency if missing
						if (color.length === 6) {
							color = 'FF'+color;
						}
						border.push('<', edge, ' style="thin">', '<color rgb="', style.borders[edge], '"/></', edge, '>');
					} else {
						border.push('<', edge, '/>');
					}
				}
				border.push('</border>');
				border = border.join('');
				// try to reuse existing border
				borderIndex = borders.indexOf(border);
				if (borderIndex < 0) {
					borderIndex = borders.push(border) - 1;
				}
			}

			// font declaration: add a new declaration and refer to it in style
			fontIndex = 0
			if (style.bold || style.italic || style.fontSize || style.fontName || style.fontColor) {
				font = ['<font>']
				if (style.bold) {
					font.push('<b/>');
				}
				if (style.italic) {
					font.push('<i/>');
				}
				font.push('<sz val="', style.fontSize || defaultFontSize, '"/>');
				var colString = 
				font.push('<color rgb="FF', style.fontColor || defaultFontColor,'"/>');
				font.push('<name val="', style.fontName || defaultFontName, '"/>');
				font.push('<family val="2"/>', '</font>');
				font = font.join('');
				// try to reuse existing font
				fontIndex = fonts.indexOf(font);
				if (fontIndex < 0) {
					fontIndex = fonts.push(font) - 1;
				}
			}
			
			//fill declaration: add a new declaration and refer to it in style
			actualFill = 0;
			if (style.fill){
				fills = fills +'<fill><patternFill patternType="solid"><fgColor rgb="FF' + style.fill + '"/>'
							+'<bgColor indexed="64"/></patternFill></fill>'
				fillNumber++;		
				actualFill = fillNumber;	
			}
			
			// declares style, and refer to optionnal formatCode, font and borders
			styles[i] = ['<xf xfId="0" borderId="', //fillId="0"  
				borderIndex, 
				'" fontId="',
				fontIndex,
				'" numFmtId="',
				style.formatCode,
				'" fillId="',
				actualFill,
				'" ',
				(style.hAlign || style.vAlign? 'applyAlignment="1" ' : ' '),
				(style.formatCode > 0 ? 'applyNumberFormat="1" ' : ' '),
				(borderIndex > 0 ? 'applyBorder="1" ' : ' '),
				(fontIndex > 0 ? 'applyFont="1" ' : ' '),
				(actualFill > 0 ? 'applyFill="1" ' : ' '),
				'>'
			];
			if (style.hAlign || style.vAlign) {
				styles[i].push('<alignment');
				if (style.hAlign) {
					styles[i].push(' horizontal="', style.hAlign, '"');
				}
				if (style.vAlign) {
					styles[i].push(' vertical="', style.vAlign, '"');
				}
				styles[i].push('/>');
			}
			styles[i].push('</xf>');
			styles[i] = styles[i].join('');
		}
		t = t.length ? '<numFmts count="' + t.length + '">' + t.join('') + '</numFmts>' : '';

		xl.file('styles.xml', '<?xml version="1.0" encoding="UTF-8" standalone="yes"?><styleSheet xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" mc:Ignorable="x14ac" xmlns:x14ac="http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac">'
			+ t + '<fonts count="'+ fonts.length + '" x14ac:knownFonts="1"><font><sz val="' + defaultFontSize + '"/><color theme="1"/><name val="' + defaultFontName + '"/><family val="2"/>'
			+ '<scheme val="minor"/></font>' + fonts.join('') + '</fonts>'
	/*count="2"*/		+ '<fills><fill><patternFill patternType="none"/></fill><fill><patternFill patternType="gray125"/></fill>' + fills + '</fills>'
			+ '<borders count="' + borders.length + '"><border><left/><right/><top/><bottom/><diagonal/></border>'
			+ borders.join('') + '</borders><cellStyleXfs count="1">'
			+ '<xf numFmtId="0" fontId="0" fillId="0" borderId="0"/></cellStyleXfs><cellXfs count="' + styles.length + '"><xf numFmtId="0" fontId="0" fillId="0" borderId="0" xfId="0"/>'
			+ styles.join('') + '</cellXfs><cellStyles count="1"><cellStyle name="Normal" xfId="0" builtinId="0"/></cellStyles><dxfs count="0"/>'
			+ '<tableStyles count="0" defaultTableStyle="TableStyleMedium2" defaultPivotStyle="PivotStyleLight16"/>'
			+ '<extLst><ext uri="{EB79DEF2-80B8-43e5-95BD-54CBDDF9020C}" xmlns:x14="http://schemas.microsoft.com/office/spreadsheetml/2009/9/main">'
			+ '<x14:slicerStyles defaultSlicerStyle="SlicerStyleLight1"/></ext></extLst></styleSheet>');

		// [Content_Types].xml
		zip.file('[Content_Types].xml', '<?xml version="1.0" encoding="UTF-8" standalone="yes"?><Types xmlns="http://schemas.openxmlformats.org/package/2006/content-types"><Default Extension="rels" ContentType="application/vnd.openxmlformats-package.relationships+xml"/><Default Extension="xml" ContentType="application/xml"/><Override PartName="/xl/workbook.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml"/>'
			+ contentTypes[0].join('') + '<Override PartName="/xl/theme/theme1.xml" ContentType="application/vnd.openxmlformats-officedocument.theme+xml"/><Override PartName="/xl/styles.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml"/><Override PartName="/xl/sharedStrings.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.sharedStrings+xml"/>'
			+ contentTypes[1].join('') + '<Override PartName="/docProps/core.xml" ContentType="application/vnd.openxmlformats-package.core-properties+xml"/><Override PartName="/docProps/app.xml" ContentType="application/vnd.openxmlformats-officedocument.extended-properties+xml"/></Types>');

		// docProps/app.xml
		docProps.file('app.xml', '<?xml version="1.0" encoding="UTF-8" standalone="yes"?><Properties xmlns="http://schemas.openxmlformats.org/officeDocument/2006/extended-properties" xmlns:vt="http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes"><Application>XLSX.js</Application><DocSecurity>0</DocSecurity><ScaleCrop>false</ScaleCrop><HeadingPairs><vt:vector size="2" baseType="variant"><vt:variant><vt:lpstr>Worksheets</vt:lpstr></vt:variant><vt:variant><vt:i4>'
			+ file.worksheets.length + '</vt:i4></vt:variant></vt:vector></HeadingPairs><TitlesOfParts><vt:vector size="' + props.length + '" baseType="lpstr"><vt:lpstr>' + props.join('</vt:lpstr><vt:lpstr>')
			+ '</vt:lpstr></vt:vector></TitlesOfParts><Manager></Manager><Company>Microsoft Corporation</Company><LinksUpToDate>false</LinksUpToDate><SharedDoc>false</SharedDoc><HyperlinksChanged>false</HyperlinksChanged><AppVersion>1.0</AppVersion></Properties>');

		// xl/_rels/workbook.xml.rels
		xl.folder('_rels').file('workbook.xml.rels', '<?xml version="1.0" encoding="UTF-8" standalone="yes"?><Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">'
			+ xlRels.join('') + '<Relationship Id="rId' + (xlRels.length + 1) + '" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/sharedStrings" Target="sharedStrings.xml"/>'
			+ '<Relationship Id="rId' + (xlRels.length + 2) + '" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles" Target="styles.xml"/>'
			+ '<Relationship Id="rId' + (xlRels.length + 3) + '" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme" Target="theme/theme1.xml"/></Relationships>');

		// xl/sharedStrings.xml
		xl.file('sharedStrings.xml', '<?xml version="1.0" encoding="UTF-8" standalone="yes"?><sst xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" count="'
			+ sharedStrings[1] + '" uniqueCount="' + sharedStrings[0].length + '"><si><t>' + sharedStrings[0].join('</t></si><si><t>') + '</t></si></sst>');

		// xl/workbook.xml
		xl.file('workbook.xml', '<?xml version="1.0" encoding="UTF-8" standalone="yes"?><workbook xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships">'
			+ '<fileVersion appName="xl" lastEdited="5" lowestEdited="5" rupBuild="9303"/><workbookPr defaultThemeVersion="124226"/><bookViews><workbookView '
			+ (file.activeWorksheet ? 'activeTab="' + file.activeWorksheet + '" ' : '') + 'xWindow="480" yWindow="60" windowWidth="18195" windowHeight="8505"/></bookViews><sheets>'
			+ worksheets.join('') + '</sheets><calcPr calcId="145621"/></workbook>');

		processTime = Date.now() - processTime;
		zipTime = Date.now();
		result = {
			base64: zip.generate({ compression: 'DEFLATE' }), zipTime: Date.now() - zipTime, processTime: processTime,
			href: function() { return 'data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,' + this.base64; }
		};
	}
	return result;
}

// NodeJs export
if (typeof exports === 'object' && typeof module === 'object') {
  module.exports = xlsx;
}

OAT.GeneratePDFOutput = function(self, fileName){
			var totalColSpan = -1;
			var tablemarginleft = 10;
			var tablemargintop = 13;
			var cellwidth = 25;
			var y = -1;
			
			//calc max length of paper
			var hgt = tablemargintop + jQuery("#" + self.controlName + "_" + self.query + " tr").length*30 + 5;
			if (hgt < 841){
				hgt = 841;
			}
			
			//calc max width of paper
			var wdt = 0;
			
			var title_row = true;
			var row_num = 0
			while ((title_row) && (row_num < jQuery("#" + self.controlName + "_" + self.query + " tr").length)){
				if (jQuery("#" + self.controlName + "_" + self.query + " tr")[row_num].getAttribute("title_row") != "true")
					title_row = false
				else
					row_num++	
			} 
			
			var tRow = jQuery("#" + self.controlName + "_" + self.query + " tr")[row_num];
			
			if (tRow == undefined){
				tRow = jQuery("#" + self.controlName + "_" + self.query + " tr")[0];
			}
			//var tRow = jQuery("#" + self.controlName + "_" + self.query + " tr")[jQuery("#" + self.controlName + "_" + self.query + " tr").length-1];
			for (var j = 0; j < tRow.children.length; j++){
				if (tRow.children[j].getAttribute('colspan') != null){
					wdt = wdt + parseInt(tRow.children[j].getAttribute('colspan'));
				} else {
					wdt = wdt + 1;
				}
			}
			wdt = tablemarginleft + wdt*(cellwidth+48) + 5;
			if (wdt < 595){
				wdt = 595;
			}
			
			
			
			//calculate columns width
			var columnsWidth = [];
			for (var i = 0; i < jQuery("#" + self.controlName + "_" + self.query + " tr").length; i++) {//for every row
				
				var colspan = -1;
				totalColSpan = -1;
				
				//calcular el desplazamiento hacia la derecha de la fila, cuando en las filas superiores hay celdas con rowspan mayor a 1
				var desp = 0;
				for(var prevR = 0; prevR < i; prevR++){	 //empiezo desde la de mas arriba
							var tRowprev = jQuery("#" + self.controlName + "_" + self.query + " tr")[prevR]; //obtengo la fila
							
							for (var prevC = 0; prevC < tRowprev.children.length; prevC++){
								var prevRowSpan = tRowprev.children[prevC].getAttribute('rowspan'); //obtengo el rowspan
								if 	((prevRowSpan != null) && (parseInt(prevRowSpan) > (i-prevR))){ //si el rowspan alcanza a esta fila
								// el desplazamiento es 1 o el colspan de la celda de arriba
									var prevColSpan = tRowprev.children[prevC].getAttribute('colspan'); //obtengo el rowspan
									if (prevColSpan===null){
										desp = 1 + desp;
									} else {
										desp = parseInt(prevColSpan) + desp;
									}
								}
							}
				}
				
				colspan = desp - 1;
				var tRow = jQuery("#" + self.controlName + "_" + self.query + " tr")[i];
				for (var j = 0; j < tRow.children.length; j++){ //for every column
					colspan++;
					var childText = tRow.children[j].textContent.replace(/^\s+|\s+$/g, '');
					var hidden    = tRow.children[j].getAttribute('hidden');
					
					if (hidden === null){
						
						var row   = tRow.children[j].getAttribute('rowspan');
						var col   = tRow.children[j].getAttribute('colspan');
						
						var oldCol = colspan;
						if (columnsWidth[oldCol] == undefined){
							columnsWidth[oldCol] = cellwidth;
						}
						
						if (((row === null) && (col === null)) || ((j === tRow.children.length-1) && (col === '3'))){
							if ((col != null) && (childText === "")){
								colspan = parseInt(col) + colspan - 1;
							}
						} else if (col === null) {
						} else if (row === null) {
							colspan = parseInt(col) + colspan - 1;
						} else {
							colspan = parseInt(col) + colspan - 1;
						}
						
						//japanese characters
						var jChar = 0;
						var carInicialesNoJap = 0
						var posC = 0;
						while ((posC < childText.length) && (childText.charCodeAt(posC) < 1000)){
							carInicialesNoJap++; 
							posC++;
						}
						jChar = childText.length - carInicialesNoJap;
						//for (var p = 0; p < childText.length; p++){
						//	if (childText.charCodeAt(p) > 1000){
						//		jChar++;
						//	}
						//} 
						
						if (jChar == 0){
							if ( (childText.length > 14) && ((col == null) || (i==0) || (parseInt(col) < 2)) ){
								if ( (childText.length * 1.68) > columnsWidth[oldCol]){
									columnsWidth[oldCol] = childText.length * 1.68;//la proporcion aurea?
								}
							}
						} else {
							//calculate for japanese space
							var w = jChar*3.10 + (childText.length-jChar)*2.00;  
							if ( ( w > columnsWidth[oldCol]) && ((col == null) || (i==0) || (parseInt(col) < 2)) ){
									columnsWidth[oldCol] = w;//la proporcion aurea?
							}
						}
					}
					totalColSpan = colspan;
				}
			}
			//recalculate widht
			var nw = 0
			for (var cWP = 0; cWP < columnsWidth.length; cWP++){
				nw = nw + columnsWidth[cWP]*2.5 + 48;
			}
			nw = tablemarginleft + nw + 5;
			if (nw > wdt){
				wdt = nw;
			}
			
			var getXOffset = function(colspan, columnsWidth){
				if (colspan > columnsWidth.length){
					colspan = columnsWidth.length
				}
				var offset = 0;
				for (var cWP = 0; cWP < colspan; cWP++){
					offset = offset + columnsWidth[cWP];
				}
				return offset;
			}
			
			var getYOffset = function(rowspan, rowsHeight){
				/*if (rowspan > rowsHeight.length){
					rowspan = rowspan.length;
				}*/
				var offsety = 0;
				for (var j = 0; j < rowspan; j++){
					offsety = offsety + columnsWith[i];
				}
				return offsety;
			}
			
			
			//draw pivot on PDF
			var doc;
			if (wdt <= 595){
				doc = new jsPDF('portrait', 'mm', 'a4', false, wdt, 792)//hgt); //landscape or portrait  //con 792 son 26 filas por hoja
			} else if ((wdt > 595) && (wdt < 792)) {
				doc = new jsPDF('landscape', 'mm', 'a4', false, 793, 792)//hgt); //landscape or portrait	
			} else {
				doc = new jsPDF('landscape', 'mm', 'a4', false, wdt, 792)//hgt); //landscape or portrait
			}
			
			
	 	    doc.setFontSize(8);
			var spanSobrantes = [];
			if (jQuery("#" + self.controlName + "_" + self.query + " tr")[1 + self.colConditions.length] != undefined){ 
				for (var n = 0; n < jQuery("#" + self.controlName + "_" + self.query + " tr")[1 + self.colConditions.length].children.length; n++){ spanSobrantes[n] = 0}
			} else {
				for (var n = 0; n < jQuery("#" + self.controlName + "_" + self.query + " tr")[self.colConditions.length].children.length; n++){ spanSobrantes[n] = 0}
			}
			for (var i = 0; i < jQuery("#" + self.controlName + "_" + self.query + " tr").length; i++) {//for every row
				//nroFilaHoja++; 
				
				var colspan = -1;
				totalColSpan = -1;
				y++;
				//calcular el desplazamiento hacia la derecha de la fila, cuando en las filas superiores hay celdas con rowspan mayor a 1
				var desp = 0;
				for(var prevR = 0; prevR < i; prevR++){	 //empiezo desde la de mas arriba
							var tRowprev = jQuery("#" + self.controlName + "_" + self.query + " tr")[prevR]; //obtengo la fila
							
							for (var prevC = 0; prevC < tRowprev.children.length; prevC++){
								var prevRowSpan = tRowprev.children[prevC].getAttribute('rowspan'); //obtengo el rowspan
								if 	((prevRowSpan != null) && (parseInt(prevRowSpan) > (i-prevR))){ //si el rowspan alcanza a esta fila
								// el desplazamiento es 1 o el colspan de la celda de arriba
									var prevColSpan = tRowprev.children[prevC].getAttribute('colspan'); //obtengo el rowspan
									if (prevColSpan===null){
										desp = 1 + desp; 
									} else {
										desp = parseInt(prevColSpan) + desp;
									}
								}
							}
				}
				
				colspan = desp - 1; 
				var tRow = jQuery("#" + self.controlName + "_" + self.query + " tr")[i];
				var jR = -1;
				for (var j = 0; j < tRow.children.length; j++){
					
					var childText = tRow.children[j].textContent.replace(/^\s+|\s+$/g, '');
					var hidden    = tRow.children[j].getAttribute('hidden');
					var ignore	  = tRow.children[j].getAttribute('pivotcorrect')
					
					
					//set styles
					var hasBackground = false;
					doc.setDrawColor(0,0,0);
					doc.setTextColor(0,0,0);
					doc.setFontStyle('normal')
					var IsTextAlignRight = (!isNaN(parseFloat(childText)) || 
								((childText == "-") && ( (tRow.children[j].getAttribute("style") == undefined) || (tRow.children[j].getAttribute("style").indexOf("text-align: left") == -1))));
					var textWidht = 0;
					textWidht = doc.getStringUnitWidth(childText) 
					if ((tRow.children[j].getAttribute("style")!=undefined) && (tRow.children[j].getAttribute("style")!=null)){
						var attributes = tRow.children[j].getAttribute("style").split(";");
						for(var at = 0; at < attributes.length; at++){
							var detail = attributes[at].split(":");
							if (detail[0].replace(/^\s+|\s+$/g, '')==="color"){
								var rgb = detail[1].replace(/^\s+|\s+$/g, '');
								rgb = rgb.substring(4,rgb.length);
								rgb = rgb.substring(0,rgb.length-1);
								doc.setTextColor( parseInt(rgb.split(",")[0]) , parseInt(rgb.split(",")[1]) , parseInt(rgb.split(",")[2]) );
								 
							} else if (detail[0].replace(/^\s+|\s+$/g, '')==="text-align"){
								//var alg = detail[1].replace(/^\s+|\s+$/g, '');
								//cellObject.vAlign = alg;
								IsTextAlignRight = (detail[1].replace(/^\s+|\s+$/g, '') == "right")
							} else if (detail[0].replace(/^\s+|\s+$/g, '')==="font-style"){
								doc.setFontStyle('italic')
							} else if (detail[0].replace(/^\s+|\s+$/g, '')==="font-weight"){
								if (detail[1].replace(/^\s+|\s+$/g, '') === "bold"){
									doc.setFontStyle('bold')
								}
							} else if (detail[0].replace(/^\s+|\s+$/g, '')==="background-color"){
								var rgb = detail[1].replace(/^\s+|\s+$/g, '');
								rgb = rgb.substring(4,rgb.length);
								rgb = rgb.substring(0,rgb.length-1);
								doc.setFillColor( parseInt(rgb.split(",")[0]) , parseInt(rgb.split(",")[1]) , parseInt(rgb.split(",")[2]) );
								//doc.setDrawColor( parseInt(rgb.split(",")[0]) , parseInt(rgb.split(",")[1]) , parseInt(rgb.split(",")[2]) );
								hasBackground = true;
							} else if (detail[0].replace(/^\s+|\s+$/g, '')==="border-color") {
								var rgb = detail[1].replace(/^\s+|\s+$/g, '');
								rgb = rgb.substring(4,rgb.length);
								rgb = rgb.substring(0,rgb.length-1);
								
								doc.setDrawColor( parseInt(rgb.split(",")[0]) , parseInt(rgb.split(",")[1]) , parseInt(rgb.split(",")[2]) );
							}
							
						}
					}
					
					
					var imgTxtData = []
					var lastTxt = "", posInitLastTxt = -1
					var previousCharCode = 0
					if (childText.charCodeAt(0) > 1000) {
						for (var cNo = 0; cNo < childText.length; cNo++) {
							if (((previousCharCode == 58) || (previousCharCode == 65306)) && (childText.charCodeAt(cNo) == 32)){
								imgTxtData[cNo] = "ToDelete"
							} else {
								imgTxtData[cNo] = OAT.getCharacterImg(childText.charCodeAt(cNo))
								previousCharCode = childText.charCodeAt(cNo)
							}
							if (childText.charCodeAt(cNo) < 1000){
								lastTxt = lastTxt + childText[cNo]
								if (posInitLastTxt == -1)	posInitLastTxt = cNo;
							} else {
								lastTxt = ""
								posInitLastTxt = -1
							}
						}
					} else {
						var posI = -1;
						//cNo = 0;
						for (var p = 1; p < childText.length; p++){
							if (childText.charCodeAt(p) > 1000){
								posI = p;
								break;
							}
						} 
						if (posI > 0){
							var tempchildText = childText.substring(0, posI);
							var posE = 0
							for (var cNo = posI; cNo < childText.length; cNo++) {
								if (childText.charCodeAt(cNo) < 1000){
									lastTxt = lastTxt + childText[cNo]
									if (posInitLastTxt == -1)	posInitLastTxt = cNo - posI;
								} else {
									lastTxt = ""
									posInitLastTxt = -1
								}
								if (((previousCharCode == 58) || (previousCharCode == 65306)) && (childText.charCodeAt(cNo) == 32))
								{
									imgTxtData[posE] = "ToDelete"
								} else {
									imgTxtData[posE] = OAT.getCharacterImg(childText.charCodeAt(cNo))
									previousCharCode = childText.charCodeAt(cNo)
								}	
								posE++;
							}
							childText = tempchildText; 
						}						
					}
					try {
						if ((posInitLastTxt != -1) && (!IsTextAlignRight)){
							imgTxtData = imgTxtData.slice(0, posInitLastTxt); 
						}
					} catch (Error){}
					
					while ((imgTxtData.length > 0) && (imgTxtData.indexOf("ToDelete") != -1)){
						var indexTDel = imgTxtData.indexOf("ToDelete")
						imgTxtData.splice(indexTDel, 1);
					}
					
					if ((hidden === null) && (!ignore)){
						
						colspan++;
						var row   = tRow.children[j].getAttribute('rowspan');
						var col   = tRow.children[j].getAttribute('colspan');
						jR = jR + 1;
						if ( row && ((y + parseInt(row)) > 26)){ //si excedo a la hoja
							var exceso = y + parseInt(row) - 26
							row = parseInt(row) - exceso
							spanSobrantes[jR + desp] = exceso 
						}
						/*for very long text*/
						var colN = 1;
						if (col!=null) colN = parseInt(col);
						if ((j === tRow.children.length-1) && (colN === 3) && (self.colConditions.length==0)) colN = 1; 
						var textinic = null; var textend = null;
						/*if (childText.length > 22*colN){
							doc.setFontSize(6);
							textinic = childText.substring(0,21);
							textend  = childText.substring(21,childText.length);
						}*/
						
						var oldCol = colspan;
						if (((row === null) && (col === null)) || ((j === tRow.children.length-1) && (col === '3') && (self.colConditions.length==0))){
							
							if (hasBackground){
								var preColsPan = colspan;
								if ((col != null) && (childText === "")){
									preColsPan = parseInt(col) + colspan - 1;
								}
								
								doc.rect(tablemarginleft - 2 + getXOffset(oldCol, columnsWidth), tablemargintop + y*10 - 7, getXOffset(preColsPan+1, columnsWidth) - getXOffset(oldCol, columnsWidth),  10, 'F');
							}
							
							var preImage = 0;
							if (childText.charCodeAt(0) < 1000){
								preImage = childText.length*1.4 + 2
								if (IsTextAlignRight){
									doc.text(tablemarginleft + getXOffset(colspan+1, columnsWidth) - textWidht*3 - 4, tablemargintop + y*10, childText);
								}else{
									doc.text(tablemarginleft + getXOffset(colspan, columnsWidth), tablemargintop + y*10, childText); 
								}
							}
							if (imgTxtData.length > 0) {
								for (var cNo = 0; cNo < imgTxtData.length; cNo++) {
									doc.addImage(imgTxtData[cNo], 'JPEG', tablemarginleft + getXOffset(colspan, columnsWidth) + preImage + cNo*2.5, tablemargintop + y*10 - 2.3, 2.5, 2.5);
								}
							} 
							
							if ((posInitLastTxt != -1) && (!IsTextAlignRight)){ //add last text
								doc.text(tablemarginleft + getXOffset(colspan, columnsWidth) + preImage + imgTxtData.length*2.5, tablemargintop + y*10, lastTxt);
							}
							
							if ((col != null) && (childText === "")){
								colspan = parseInt(col) + colspan - 1;
							}
							//draw rectangle border
							doc.line(tablemarginleft - 2 + getXOffset(oldCol, columnsWidth), tablemargintop + y*10 - 7, tablemarginleft - 2 + getXOffset((colspan+1), columnsWidth), tablemargintop + y*10 - 7); //top border
							doc.line(tablemarginleft - 2 + getXOffset(oldCol, columnsWidth), tablemargintop + y*10 - 7, tablemarginleft - 2 + getXOffset(oldCol, columnsWidth)     , tablemargintop + y*10 + 3); //left border
							
						} else if (col === null) {
							
							if (hasBackground){
								if (row === null){
									doc.rect(tablemarginleft - 2 + getXOffset(colspan, columnsWidth), tablemargintop + y*10 - 7, cellwidth, 10, 'F');
								} else {
									doc.rect(tablemarginleft - 2 + getXOffset(colspan, columnsWidth), tablemargintop + y*10 - 7, cellwidth, 10 + ( parseInt(row) -1 )*10, 'F');
								}
							}
							var preImage = 0;
							if (childText.charCodeAt(0) < 1000){
								preImage = childText.length*1.4 + 2
								doc.text(tablemarginleft + getXOffset(colspan, columnsWidth), tablemargintop + y*10, childText);
								
							}
							if (imgTxtData.length > 0) {
								for (var cNo = 0; cNo < imgTxtData.length; cNo++) {
									doc.addImage(imgTxtData[cNo], 'JPEG', tablemarginleft + getXOffset(colspan, columnsWidth) + preImage + cNo*2.5, tablemargintop + y*10 - 2.3, 2.5, 2.5);
								}
							}
							
							if ((posInitLastTxt != -1) && (!IsTextAlignRight)){ //add last text
								doc.text(tablemarginleft + getXOffset(colspan, columnsWidth) + preImage + imgTxtData.length*2.5, tablemargintop + y*10, lastTxt);
							}
							 
							doc.line(tablemarginleft - 2 + getXOffset(colspan, columnsWidth), tablemargintop + y*10 - 7, tablemarginleft - 2 + getXOffset((colspan+1), columnsWidth) , tablemargintop + y*10 - 7); //top border
							if (row === null){
								doc.line(tablemarginleft - 2 + getXOffset(colspan, columnsWidth), tablemargintop + y*10 - 7, tablemarginleft - 2 + getXOffset(colspan, columnsWidth)    , tablemargintop + y*10 + 3); //left border
							} else {
								doc.line(tablemarginleft - 2 + getXOffset(oldCol, columnsWidth), tablemargintop + y*10 - 7, tablemarginleft - 2 +  getXOffset(oldCol, columnsWidth)     , tablemargintop + (y+parseInt(row)-1)*10 + 3); //left border
							}
						} else if (row === null) {
							
							if (hasBackground){
								var prevColsSpan = parseInt(col) + colspan - 1;
								doc.rect(tablemarginleft - 2 + getXOffset(oldCol, columnsWidth), tablemargintop + y*10 - 7, getXOffset((prevColsSpan+1), columnsWidth) - getXOffset(oldCol, columnsWidth),  10, 'F');
							}
							var preImage = 0;
							if (childText.charCodeAt(0) < 1000){
								preImage = childText.length*1.4 + 2
								doc.text(tablemarginleft + getXOffset(colspan, columnsWidth), tablemargintop + y*10, childText);
								
							}
							if (imgTxtData.length > 0) {
								for (var cNo = 0; cNo < imgTxtData.length; cNo++) {
									doc.addImage(imgTxtData[cNo], 'JPEG', tablemarginleft + getXOffset(colspan, columnsWidth) + preImage + cNo*2.5, tablemargintop + y*10 - 2.3, 2.5, 2.5);
								}
							}
							
							if ((posInitLastTxt != -1) && (!IsTextAlignRight)){ //add last text
								doc.text(tablemarginleft + getXOffset(colspan, columnsWidth) + preImage + imgTxtData.length*2.5, tablemargintop + y*10, lastTxt);
							}
							 
							colspan = parseInt(col) + colspan - 1;
							
							doc.line(tablemarginleft - 2 + getXOffset(oldCol, columnsWidth), tablemargintop + y*10 - 7, tablemarginleft - 2 + getXOffset((colspan+1), columnsWidth)    , tablemargintop + y*10 - 7); //top border
							doc.line(tablemarginleft - 2 + getXOffset(oldCol, columnsWidth), tablemargintop + y*10 - 7, tablemarginleft - 2 + getXOffset(oldCol, columnsWidth)     , tablemargintop + y*10 + 3); //left border
						} else {
							if (hasBackground){
								var prevColsSpan = parseInt(col) + colspan - 1;
								if (row === null){
									doc.rect(tablemarginleft - 2 + getXOffset(oldCol, columnsWidth), tablemargintop + y*10 - 7, getXOffset((prevColsSpan+1), columnsWidth) - getXOffset(oldCol, columnsWidth), 10, 'F');
								} else {
									doc.rect(tablemarginleft - 2 + getXOffset(oldCol, columnsWidth), tablemargintop + y*10 - 7, getXOffset((prevColsSpan+1), columnsWidth) - getXOffset(oldCol, columnsWidth), ( parseInt(row)-1 )*10, 'F');
								}
							}
							var preImage = 0;
							if (childText.charCodeAt(0) < 1000){
								preImage = childText.length*1.4 + 2
								doc.text(tablemarginleft /*- 2*/ + getXOffset(colspan, columnsWidth), tablemargintop + y*10, childText);
							}
							if (imgTxtData.length > 0) {
								for (var cNo = 0; cNo < imgTxtData.length; cNo++) {
									doc.addImage(imgTxtData[cNo], 'JPEG', tablemarginleft /*- 2*/ + getXOffset(colspan, columnsWidth) + preImage + cNo*2.5, tablemargintop + y*10 - 2.3, 2.5, 2.5);
								}
							}
							
							if ((posInitLastTxt != -1) && (!IsTextAlignRight)){ //add last text
								doc.text(tablemarginleft /*- 2*/ + getXOffset(colspan, columnsWidth) + preImage + imgTxtData.length*2.5, tablemargintop + y*10, lastTxt);
							} 
							
							colspan = parseInt(col) + colspan - 1;
							
							doc.line(tablemarginleft - 2 + getXOffset(oldCol, columnsWidth), tablemargintop + y*10 - 7, tablemarginleft - 2 + getXOffset((colspan+1), columnsWidth), tablemargintop + y*10 - 7); //top border
							if (row === null){
								doc.line(tablemarginleft - 2 + getXOffset(oldCol, columnsWidth), tablemargintop + y*10 - 7, tablemarginleft - 2 + getXOffset(oldCol, columnsWidth)     , tablemargintop + y*10 + 3); //left border
							} else {
								doc.line(tablemarginleft - 2 + getXOffset(oldCol, columnsWidth), tablemargintop + y*10 - 7, tablemarginleft - 2 + getXOffset(oldCol, columnsWidth)     , tablemargintop + (y+parseInt(row)-1)*10 + 3); //left border
							}
						}
					}
					
					doc.setFontSize(8);
					
					totalColSpan = colspan;
					//last right border
					if (j === tRow.children.length -1){
						doc.line(tablemarginleft - 2 + getXOffset((colspan+1), columnsWidth), tablemargintop + y*10 - 7, tablemarginleft - 2 + getXOffset((colspan+1), columnsWidth)     , tablemargintop + y*10 + 3);
					}
					
				}
				if (y >= 25){
					doc.setDrawColor(0,0,0)
					//bottom last line
					doc.line(tablemarginleft - 2, tablemargintop + (y+1)*10 - 7, tablemarginleft - 2 + getXOffset((totalColSpan+1), columnsWidth), tablemargintop + (y+1)*10 - 7);
					y = 0
					doc.addPage()
					//add sobrantes
					for (var t = 0; t < spanSobrantes.length; t++){
						if (!isNaN(spanSobrantes[t])){
							if (spanSobrantes[t] >= 25){
								spanSobrantes[t] = spanSobrantes[t] - 25
								doc.line(tablemarginleft - 2 + getXOffset(t, columnsWidth), tablemargintop + (1)*10 - 7, tablemarginleft - 2 + getXOffset((t+1), columnsWidth), tablemargintop + (1)*10 - 7); //top border
								doc.line(tablemarginleft - 2 + getXOffset(t, columnsWidth), tablemargintop + (1)*10 - 7, tablemarginleft - 2 +  getXOffset(t, columnsWidth)     , tablemargintop + ((1)+ 25 -1)*10 + 3); //left border
							} else {
								doc.line(tablemarginleft - 2 + getXOffset(t, columnsWidth), tablemargintop + (1)*10 - 7, tablemarginleft - 2 + getXOffset((t+1), columnsWidth), tablemargintop + (1)*10 - 7); //top border
								doc.line(tablemarginleft - 2 + getXOffset(t, columnsWidth), tablemargintop + (1)*10 - 7, tablemarginleft - 2 +  getXOffset(t, columnsWidth)     , tablemargintop + ((1)+spanSobrantes[t]-1)*10 + 3); //left border
								spanSobrantes[t] = 0
							}
						}
					}
					//spanSobrantes = [] 
				}	
				
			}
			doc.setDrawColor(0,0,0)
			//bottom last line
			doc.line(tablemarginleft - 2, tablemargintop + (y+1)*10 - 7, tablemarginleft - 2 + getXOffset((totalColSpan+1), columnsWidth), tablemargintop + (y+1)*10 - 7);
	
			if ((gx.util.browser.webkit) && (!gx.util.browser.chrome)){ //for safari
				doc.output('dataurlnewwindow');	
			} else if (!gx.util.browser.isIE() || (9<gx.util.browser.ieVersion())){ 
				doc.save(fileName+'.pdf');
			} else {
				return doc.output();
			}
			
	
}

removeComas = function(value, self){ //TODO: also remove $ and none digital or . characters
		var newValue = ""; actPos = 0;
		
		var decimalSeparator = self.defaultPicture.getAttribute("decimalSeparator");
		
		for (var poVal = 0; poVal < value.length; poVal++){
			if ((value[poVal] == "0") || (value[poVal] == "1") || (value[poVal] == "2") 
				|| (value[poVal] == "3") || (value[poVal] == "4") || (value[poVal] == "5") 
					|| (value[poVal] == "6") || (value[poVal] == "7") || (value[poVal] == "8")
						|| (value[poVal] == "9") || (value[poVal] == decimalSeparator/*"."*/) || (value[poVal] == "-")){
				newValue = newValue + value[poVal];
			}
		}
		
		if (decimalSeparator != "."){
			var tempValue = "";
			for (var poVal = 0; poVal < newValue.length; poVal++){
				if (newValue[poVal] == decimalSeparator){
					tempValue = tempValue + '.';
				} else {
					tempValue = tempValue + newValue[poVal];
				}
			}
			newValue = tempValue;
		}
		//while (value.indexOf(",") != -1){
		//	value = value.slice(0, value.indexOf(",")) + value.slice(value.indexOf(",")+1); 
		//}
		return newValue;
}

cleanDistinctValue = function(value, self, ind){
	if (value == "#NuN#")
	{
		return " ";
	} else {
		if (value.indexOf("%")>-1){
			return value.replace(/\&/g, "&amp;").replace(/"/g, "&quot;").replace(/\%/g,"%"+ind).trim() //replace(/\%/g,"percent").	
		} else {
			return value.replace(/\&/g, "&amp;").replace(/"/g, "&quot;").trim() //replace(/\%/g,"percent").
		}	
	}
}

createExcelROWITEMS = function(item ,level, pfvi, lastRowLevel, originalLevel){
    	if (level === 0){
    		var index = pfvi[lastRowLevel].indexOf(item.value);
    		var rs = ""
    		if (originalLevel===0){
    			rs = "<i>" 
    		}else{
    			rs = "<i r=\""+ originalLevel +"\">";
    		}
    		if (index > 0){
    			return  rs + "<x v=\"" + index + "\"/></i>";
    		} else {
    			return  rs +"<x/></i>";
    		}
    	} else {
    		level--;
    		var str = ""
    		for(var i = 0; i < item.items.length; i++){
    			str = str + createExcelROWITEMS(item.items[i], level, pfvi, lastRowLevel, originalLevel); 
    		}
    		return str;
    	}
}
    
    createExcelCOLITEMS = function(item ,level, pfvi, lastRowLevel, originalLevel){
    	if (level === 0){
    		var index = pfvi[lastRowLevel].indexOf(item.value.trim());
    		var rs = ""
    		for (var iCV = 0; iCV < measures.length; iCV++) {
    			if (originalLevel===0){
    				rs = "<i>" 
    			}else{
    				rs = "<i r=\""+ originalLevel +"\">";
    			}
    		//if (index > 0){
    			rs =  rs + "<x v=\"" + index + "\"/>";
    		//} else {
    		//	rs =  rs +"<x/></i>";
    		//}
    			if (measures.length > 1){
    		 		rs = rs+ "<x/>";
    		 	}
    		 	rs = rs+ "</i>";
    		}
    		return rs;
    	} else {
    		level--;
    		var str = ""
    		for(var i = 0; i < item.items.length; i++){
    			str = str + createExcelROWITEMS(item.items[i], level, pfvi, lastRowLevel, originalLevel); 
    		}
    		return str;
    	}
    }

OAT.GenerateExcelOutput = function(fileName, self){ 
		function componentToHex(c) {
    		var hex = c.toString(16);
    		return hex.length == 1 ? "0" + hex : hex;
		}

		function rgbToHex(r, g, b) {
    		return componentToHex(r) + componentToHex(g) + componentToHex(b);
		}

		function getStyles(attributes){
			var styleObject = {};
			for(var at = 0; at < attributes.length; at++){
				var detail = attributes[at].split(":");
				if (detail[0].replace(/^\s+|\s+$/g, '')==="color"){
					var rgb = detail[1].replace(/^\s+|\s+$/g, '');
					rgb = rgb.substring(4,rgb.length);
					rgb = rgb.substring(0,rgb.length-1);
					var hex =rgbToHex(parseInt(rgb.split(",")[0]),parseInt(rgb.split(",")[1]),parseInt(rgb.split(",")[2]))
					styleObject.fontColor = hex; 
				} else if (detail[0].replace(/^\s+|\s+$/g, '')==="text-align"){
					var alg = detail[1].replace(/^\s+|\s+$/g, '');
					styleObject.vAlign = alg;
				} else if (detail[0].replace(/^\s+|\s+$/g, '')==="font-style"){
					if (detail[1].replace(/^\s+|\s+$/g, '') === "italic"){
						styleObject.italic = 1;
					}
				} else if (detail[0].replace(/^\s+|\s+$/g, '')==="font-weight"){
					if (detail[1].replace(/^\s+|\s+$/g, '') === "bold"){
						styleObject.bold = 1;
					}
				} else if (detail[0].replace(/^\s+|\s+$/g, '')==="background-color"){
					var rgb = detail[1].replace(/^\s+|\s+$/g, '');
					rgb = rgb.substring(4,rgb.length);
					rgb = rgb.substring(0,rgb.length-1);
					var hex =rgbToHex(parseInt(rgb.split(",")[0]),parseInt(rgb.split(",")[1]),parseInt(rgb.split(",")[2]))
					styleObject.fill = hex;
				}
			}
			return styleObject;
		}
		
		var getMeasureIdentifier = function(self, value){
					for (var name in self.formulaInfo.itemPosition) {
  						if (self.formulaInfo.itemPosition[name] == value){
  							return name;	
  						}
					}
				}
				
		dataTable = [];
		//headers
		dataRow = [];
  		for(var col=0; col < self.columns.length; col++){
  			 var cellObject = {value: cleanDistinctValue(self.columns[col].getAttribute("displayName"),self, self.columns[col].getAttribute("name")) }; 
  			 cellObject.isString = 1;
			 dataRow.push( cellObject )
		}
		for (var iCV = 0; iCV < measures.length; iCV++) {
			if (!self.formulaInfo.measureFormula[iCV].hasFormula){
				var cellObject = {value: cleanDistinctValue(measures[iCV].getAttribute("displayName"), self, measures[iCV].getAttribute("name")) };
				dataRow.push( cellObject )
			}
		}	
		
		var formMeasures = [];
		if (self.formulaInfo.cantFormulaMeasures > 0){
			for (var fI = 0; fI < self.formulaInfo.measureFormula.length; fI++){
				if (self.formulaInfo.measureFormula[fI].hasFormula){
					for(var mI = 0; mI < self.formulaInfo.measureFormula[fI].relatedMeasures.length; mI++){
						var numMeasure = self.formulaInfo.measureFormula[fI].relatedMeasures[mI]
						if (formMeasures.indexOf(numMeasure) == -1) {
							formMeasures.push(numMeasure)
						}
					}
				}
			}
			formMeasures.sort();
			for (var iCV = 0; iCV < formMeasures.length; iCV++) {
				var cellObject = {value: getMeasureIdentifier(self, formMeasures[iCV]) };
				dataRow.push( cellObject )
			}
		}
		
		dataTable.push(dataRow);
		
		var dataToShow = self.GeneralDataRows//self.filteredData
		if (self.autoPaging) {
			dataToShow = self.RowsWhenMoveToFilter
		}
		if (self.formulaInfo.cantFormulaMeasures > 0){
			dataToShow = self.recordForFormula
		}
		
		
		
		//values
		for(var rN = 0; rN < dataToShow.length; rN++){
			dataRow = [];
			for(var col=0; col < self.columns.length; col++){
				//var pos = parseInt(this.columns[col].getAttribute("dataField").substring(1, this.columns[col].getAttribute("dataField").length)) -1;
				//if (isNaN(pos)){
				var pos = self.conditions[col].dataRowPosition
				//}
				var value = dataToShow[rN][pos];  //value is parse in PDJ.js
				if (value == "#NuN#") { value = " "}
				var cellObject = {value: value};
				cellObject.isString = 1;
				
				var th = OAT.Dom.create("th", {}, "");
				th = self.applyFormatValues(th, dataToShow[rN][pos], pos)
				if (th.getAttribute("style")!=undefined){
					var attributes = th.getAttribute("style").split(";");
					for(var at = 0; at < attributes.length; at++){
						var detail = attributes[at].split(":");
						if (detail[0].replace(/^\s+|\s+$/g, '')==="color"){
							var rgb = detail[1].replace(/^\s+|\s+$/g, '');
							rgb = rgb.substring(4,rgb.length);
							rgb = rgb.substring(0,rgb.length-1);
							var hex =rgbToHex(parseInt(rgb.split(",")[0]),parseInt(rgb.split(",")[1]),parseInt(rgb.split(",")[2]))
							cellObject.fontColor = hex; 
						} else if (detail[0].replace(/^\s+|\s+$/g, '')==="text-align"){
							var alg = detail[1].replace(/^\s+|\s+$/g, '');
							cellObject.vAlign = alg;
						} else if (detail[0].replace(/^\s+|\s+$/g, '')==="font-style"){
							if (detail[1].replace(/^\s+|\s+$/g, '') === "italic"){
								cellObject.italic = 1;
							}
						} else if (detail[0].replace(/^\s+|\s+$/g, '')==="font-weight"){
							if (detail[1].replace(/^\s+|\s+$/g, '') === "bold"){
								cellObject.bold = 1;
							}
						} else if (detail[0].replace(/^\s+|\s+$/g, '')==="background-color"){
							var rgb = detail[1].replace(/^\s+|\s+$/g, '');
							rgb = rgb.substring(4,rgb.length);
							rgb = rgb.substring(0,rgb.length-1);
							var hex =rgbToHex(parseInt(rgb.split(",")[0]),parseInt(rgb.split(",")[1]),parseInt(rgb.split(",")[2]))
							cellObject.fill = hex; 
						}
					}
				}
				dataRow.push( cellObject )
			}
			for (var iCV = 0; iCV < measures.length; iCV++) {
				if (!self.formulaInfo.measureFormula[iCV].hasFormula)	
				{
					var	pos = self.columns.length + iCV;
				
					var value = dataToShow[rN][pos];
					value = self.defaultPictureValue(value, (iCV == measures.length-1), iCV)
					value = removeComas(value, self); 
					var cellObject = {value: value};
								
					var td = OAT.Dom.create("td", {}, "");
					td = self.applyConditionalFormats(td, dataToShow[rN][pos], (iCV === (measures.length-1))?true:false, iCV) 
					if (td.getAttribute("style")!=undefined){
						var attributes = td.getAttribute("style").split(";");
						for(var at = 0; at < attributes.length; at++){
							var detail = attributes[at].split(":");
							if (detail[0].replace(/^\s+|\s+$/g, '')==="color"){
								var rgb = detail[1].replace(/^\s+|\s+$/g, '');
								rgb = rgb.substring(4,rgb.length);
								rgb = rgb.substring(0,rgb.length-1);
								var hex =rgbToHex(parseInt(rgb.split(",")[0]),parseInt(rgb.split(",")[1]),parseInt(rgb.split(",")[2]))
								cellObject.fontColor = hex; 
							} else if (detail[0].replace(/^\s+|\s+$/g, '')==="text-align"){
								var alg = detail[1].replace(/^\s+|\s+$/g, '');
								cellObject.vAlign = alg;
							} else if (detail[0].replace(/^\s+|\s+$/g, '')==="font-style"){
								if (detail[1].replace(/^\s+|\s+$/g, '') === "italic"){
									cellObject.italic = 1;
								}
							} else if (detail[0].replace(/^\s+|\s+$/g, '')==="font-weight"){
								if (detail[1].replace(/^\s+|\s+$/g, '') === "bold"){
									cellObject.bold = 1;
								}
							} else if (detail[0].replace(/^\s+|\s+$/g, '')==="background-color"){
								var rgb = detail[1].replace(/^\s+|\s+$/g, '');
								rgb = rgb.substring(4,rgb.length);
								rgb = rgb.substring(0,rgb.length-1);
								var hex =rgbToHex(parseInt(rgb.split(",")[0]),parseInt(rgb.split(",")[1]),parseInt(rgb.split(",")[2]))
								cellObject.fill = hex; 
							}
						}
					}
					dataRow.push( cellObject )
				}
			}
			
			for (var formItem = 0; formItem < formMeasures.length; formItem++){
				dataRow.push({ value: dataToShow[rN][formMeasures[formItem]] })
			}
			
			dataTable.push(dataRow);
		} 
		
	
		var stylesList = [];
		var columnWithStyle = [];
		var styleId = 1
		for (var col = 0; col < self.columns.length; col++){
			columnWithStyle[col] = 0;
			var th = OAT.Dom.create("th", {}, "");
			//var pos = parseInt(this.columns[col].getAttribute("dataField").substring(1, this.columns[col].getAttribute("dataField").length)) -1;
			//if (isNaN(pos)){
			var	pos = self.conditions[col].dataRowPosition
			//}
			th = self.applyFormatValues(th, dataToShow[0][pos], pos)
			
			if (th.getAttribute("style")!=undefined){
				var attributes = th.getAttribute("style").split(";");
				var styleObject = getStyles(attributes);
				if ((styleObject.fontColor != undefined) || (styleObject.italic != undefined) || (styleObject.bold != undefined)){
					columnWithStyle[col] = styleId;
					styleId++;
					stylesList.push(styleObject);
				}
			}
		}
		
		var measuresWidthStyle = [];
		for (var ms = 0; ms < measures.length; ms++){
			measuresWidthStyle[ms] = 0;
			var td = OAT.Dom.create("td", {}, "");
			//var pos = parseInt(measures[ms].getAttribute("dataField").substring(1, measures[ms].getAttribute("dataField").length /*- 1*/)) -1;
			//if (isNaN(pos)){
			var pos = self.columns.length + ms;
			//}
			td = self.applyConditionalFormats(td, dataToShow[0][pos], (ms === (measures.length-1))?true:false, ms)
			
			if (td.getAttribute("style")!=undefined){
				var attributes = td.getAttribute("style").split(";");
				var styleObject = getStyles(attributes);
				if ((styleObject.fontColor != undefined) || (styleObject.italic != undefined) || (styleObject.bold != undefined)){
					measuresWidthStyle[ms] = styleId;
					styleId++;
					stylesList.push(styleObject);
				}
			}
		}
		
		
		
		var withStyles = (stylesList.length > 0);
		var letras = ['A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z','AA','AB','AC','AD'];
		var lastPos = letras[(measures.length + self.columns.length)-1] +(dataToShow.length+1+2);
		//xl/pivotTables/pivotTable1.xml string definition
		var firstDataRow = self.colConditions.length + 2;
		var firstDataCol = (((self.colConditions.length > 0)) ? self.colConditions.length : 1);
		
		var hasFormula = false;
		var totalsExportPivotPdfOptions = " ";
		for ( var fIdx = 0; fIdx < self.formulaInfo.measureFormula.length; fIdx++) {
			if (self.formulaInfo.measureFormula[fIdx].hasFormula){
				hasFormula = true;
			}
		} //rowGrandTotals="0" colGrandTotals="0"
		//if (hasFormula){
			//totalsExportPivotPdfOptions = 'rowGrandTotals="0" colGrandTotals="0"'
		//}
		
		var dataToShow = self.GeneralDataRows
		if (self.autoPaging) {
			dataToShow = self.RowsWhenMoveToFilter
		}
		
		var sheet;
		//--------------
		if (self.columns.length < 0){
		
			var xlpivotTable1 = '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>'
							+'<pivotTableDefinition name="Tabla dinamica1" cacheId="5" dataCaption="'+gx.getMessage("GXPL_QViewerJSValue")+'" createdVersion="3" updatedVersion="3" minRefreshableVersion="3" applyNumberFormats="0" applyBorderFormats="0" applyFontFormats="0" applyPatternFormats="0" applyAlignmentFormats="0" applyWidthHeightFormats="1" showCalcMbrs="0" useAutoFormatting="1" itemPrintTitles="1" indent="0" outline="1" outlineData="1" multipleFieldFilters="0" rowHeaderCaption="Make" ' + totalsExportPivotPdfOptions + 'xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main">'
							+"<location ref=\"A1:"+ lastPos +"\" firstHeaderRow=\"1\" firstDataRow=\""+ firstDataRow +"\" firstDataCol=\""+ firstDataCol +"\"/>"
													+ "<pivotFields>"
			var pfi = [];
			var pfvi = [];											
			var pfviExclude = [];
			
			var rowLevel = -1;
			var lastRowLevel = 0; 
			var colLevel = -1; 
			var lastColLevel = 0;
			for(var iCV=0; iCV < self.columns.length; iCV++){
    			
    			var position = 'axisRow';
    			if (self.rowConditions.find(iCV)!=-1){
    				position = 'axisRow';
    				rowLevel++;
    				lastRowLevel = iCV;
    			} else if (self.colConditions.find(iCV)!=-1) {
    				position = 'axisCol';
    				colLevel++;
    				lastColLevel = iCV;
    			} else {
    				position = 'axisRow';
    				rowLevel++;
    				lastRowLevel = iCV;
    			}
    			
    			var formulaOptionString = '';
    			var formulaItemString = "<item t=\"default\"/>"
    			//if (hasFormula){
    				//formulaOptionString = 'defaultSubtotal=\"0\"'
    				//formulaItemString = ""
    			//}  
    			xlpivotTable1 = xlpivotTable1 + "<pivotField axis=\"" + position + "\" showAll=\"0\" " + formulaOptionString + ">" + //numFmtId=\"" + 163 + "\"
    								"<items>"
    			var pfvl; var pfvlExclude;
    			if (self.conditions[iCV].distinctValues != undefined){
    				pfvl = []; pfvlExclude = [];
    				for (var val = 0; val < self.conditions[iCV].distinctValues.length; val++){
    					//pfvl.push(self.conditions[iCV].distinctValues[val].replace(/\&/g, "&amp;").replace(/"/g, "&quot;"));
    					pfvl.push(cleanDistinctValue(self.conditions[iCV].distinctValues[val], self));
    					var hiddenString = "";
    					if (self.conditions[iCV].blackList.find(self.conditions[iCV].distinctValues[val]) != -1){
    						hiddenString = " h=\"1\"";
    						pfvlExclude.push(cleanDistinctValue(self.conditions[iCV].distinctValues[val], self));
    					}
    					xlpivotTable1 = xlpivotTable1 + "<item x=\""+ val +"\" "+ hiddenString +" />";
    				} 
    				xlpivotTable1 = xlpivotTable1 + formulaItemString//"<item t=\"default\"/>"
    								+ "</items>"
    								+ "</pivotField>";
    			}
    			pfvi[iCV] = pfvl;
    			pfviExclude[iCV] = pfvlExclude[iCV]
    			pfi.push(iCV);	
    		}
    	
    		for (var iCV = 0; iCV < measures.length; iCV++) {
				xlpivotTable1 = xlpivotTable1 + "<pivotField dataField=\"1\" showAll=\"0\"/>"; //numFmtId=\"" + 163 + "\"
				pfi.push(cleanDistinctValue(measures[iCV].getAttribute("displayName"),self, measures[iCV].getAttribute("name")));
			}	
    	
    		xlpivotTable1 = xlpivotTable1 + "</pivotFields>"
    		xlpivotTable1 = xlpivotTable1 + "<rowFields>"
			for(var iCV=0; iCV < self.columns.length; iCV++){
				if ((self.rowConditions.find(iCV)!=-1) || (self.filterIndexes.find(iCV) !=-1)){
					xlpivotTable1 = xlpivotTable1 + "<field x=\"" + 0 + "\"/>";
				}
			}
			xlpivotTable1 = xlpivotTable1 + "</rowFields>"
		
			xlpivotTable1 = xlpivotTable1 + "<rowItems>"
			for (var it = 0; it < self.rowStructure.items.length; it++){
				xlpivotTable1 = xlpivotTable1 + createExcelROWITEMS(self.rowStructure.items[it], rowLevel, pfvi, lastRowLevel, rowLevel)
			}
			xlpivotTable1 = xlpivotTable1 + "<i t=\"grand\"><x/></i>"
									  + "</rowItems>"
		
			if ((((colLevel > -1)) || ((measures.length > 1)))){
				xlpivotTable1 = xlpivotTable1 + "<colFields>"
		
				for(var iCV=0; iCV < self.columns.length; iCV++){
					if (self.colConditions.find(iCV)!=-1){
						var pos = pfi.indexOf(iCV);
						xlpivotTable1 = xlpivotTable1 + "<field x=\"" + pos + "\"/>";
					}
		 		}
		 		if (measures.length > 1){
            	     xlpivotTable1 = xlpivotTable1 + "<field x=\"-2\"/>";
         		};
    		
  				xlpivotTable1 = xlpivotTable1 + "</colFields>"				
			}		
		
			//col items
			xlpivotTable1 = xlpivotTable1 + "<colItems>"
			if (self.colStructure.items){
				for (var it = 0; it < self.colStructure.items.length; it++){
    				xlpivotTable1 = xlpivotTable1 + createExcelCOLITEMS(self.colStructure.items[it], colLevel, pfvi, lastColLevel, colLevel)
    			}				
			} else {
				for (var iCV = 0; iCV < measures.length; iCV++) {
					xlpivotTable1 = xlpivotTable1 + '<i/>';
				}
			}
			xlpivotTable1 = xlpivotTable1 + "</colItems>"
		
			xlpivotTable1 = xlpivotTable1 + "<dataFields>"
    			for (var iCV = 0; iCV < measures.length; iCV++) {
    				xlpivotTable1 = xlpivotTable1 + "<dataField name=\""+ cleanDistinctValue(measures[iCV].getAttribute("displayName"),self,measures[iCV].getAttribute("name")) +"\" fld=\"" + (iCV+self.columns.length) + "\"/>" //\" subtotal=\"sum\" numFmtId=\"163\"		
    			}
    		
  			xlpivotTable1 =	xlpivotTable1 + "</dataFields>"
  		
  		
			if(withStyles) {
				xlpivotTable1 = xlpivotTable1 + '<formats>'
				//format for columns
				for(var iCV = 0; iCV < self.columns.length; iCV++) {
					if(columnWithStyle[iCV] > 0) {
						xlpivotTable1 = xlpivotTable1 + '<format dxfId="' + columnWithStyle[iCV] + '">' + '<pivotArea dataOnly="0" labelOnly="1" fieldPosition="0">' + '<references ><reference field="0">'

						if(self.conditions[iCV].distinctValues != undefined) {
							for(var val = 0; val < self.conditions[iCV].distinctValues.length; val++) {
								xlpivotTable1 = xlpivotTable1 + '<x v="' + val + '"/>';
							}
						}
						xlpivotTable1 = xlpivotTable1 + '</reference></references>' + '</pivotArea>' + '</format>'
					}
				}

				for(var it = 0; it < measures.length; it++) {
					if(measuresWidthStyle[it] > 0) {
						xlpivotTable1 = xlpivotTable1 + '<format dxfId="' + measuresWidthStyle[it] + '">'
						xlpivotTable1 = xlpivotTable1 + '<pivotArea  fieldPosition="0">'
						xlpivotTable1 = xlpivotTable1 + '<references>'
						xlpivotTable1 = xlpivotTable1 + '<reference field="4294967294" selected="0">'
						xlpivotTable1 = xlpivotTable1 + '<x v="' + it + '"/></reference>'
						xlpivotTable1 = xlpivotTable1 + '<reference field="0">'
						for(var p = 0; p < self.rowStructure.items.length; p++) {
							xlpivotTable1 = xlpivotTable1 + '<x v="' + p + '"/>'
						}
						xlpivotTable1 = xlpivotTable1 + '</reference>'
						xlpivotTable1 = xlpivotTable1 + '</references></pivotArea>'
						xlpivotTable1 = xlpivotTable1 + '</format>'
					}
				}
				xlpivotTable1 = xlpivotTable1 + '</formats>'
			}
		
	  		xlpivotTable1 =	xlpivotTable1 +  "<pivotTableStyleInfo name=\"PivotStyleLight16\" showRowHeaders=\"1\" showColHeaders=\"1\" showRowStripes=\"0\" showColStripes=\"0\" showLastColumn=\"1\"/>"
	  			+ "</pivotTableDefinition>"
		
			var xlstyle = '<dxf><font><color rgb="FFCCCCCC" /></font></dxf>';
			if(withStyles) {
				for(var s = 0; s < stylesList.length; s++) {
					xlstyle = xlstyle + '<dxf><font>'
					if(stylesList[s].bold != undefined) {
						xlstyle = xlstyle + '<b/>'
					}
					if(stylesList[s].italic != undefined) {
						xlstyle = xlstyle + '<i/>'
					}
					if(stylesList[s].fontColor != undefined) {
						xlstyle = xlstyle + '<color rgb="FF' + stylesList[s].fontColor + '"/>'
					}
					xlstyle = xlstyle + '</font></dxf>';
				}
			}
			var dfxStr = xlstyle;  
			//xl/pivotCache/cacheRecords
			var cacheRecords = '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>' 
						+ '<pivotCacheRecords xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships">'
			for(var rN = 0; rN < dataToShow.length; rN++){
				cacheRecords = 	cacheRecords + "<r>"
			
				for(var col=0; col < self.columns.length; col++){
					var vvalue = pfvi[col].indexOf(dataToShow[rN][col])
					cacheRecords = 	cacheRecords + '<x v="' + vvalue + '"/>'
				}	
			
				for (var iCV = 0; iCV < measures.length; iCV++) {
					var	pos = self.columns.length + iCV;
					var value = self.defaultPictureValue(dataToShow[rN][pos], (iCV == measures.length-1), iCV) 
					value = removeComas(value, self);
					value = cleanDistinctValue(value,self)
					//value.replace(/\&/g, "&amp;").replace(/"/g, "&quot;")
					cacheRecords = 	cacheRecords + '<n v=\"' + value + '"/>';
				}
				cacheRecords = 	cacheRecords + "</r>" 	
			}
			cacheRecords = cacheRecords + '</pivotCacheRecords>'; 
		
		
			//xl/pivotCache/cacheDefinition
			var cacheDefinition = '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>'
							+ '<pivotCacheDefinition r:id="rId1" refreshOnLoad="1" xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships">'
  							+ '<cacheSource type="worksheet">'
  							
			cacheDefinition = cacheDefinition + '<worksheetSource ref="A1:' + letras[self.columns.length + measures.length -1] + (dataToShow.length+1) + '" sheet="SourceData"/>'
			cacheDefinition = cacheDefinition + '</cacheSource>'
			cacheDefinition = cacheDefinition + '<cacheFields count="' + (self.columns.length + measures.length) + '">'
			for(var col=0; col < self.columns.length; col++){
				cacheDefinition = cacheDefinition + '<cacheField name="' + cleanDistinctValue(self.columns[col].getAttribute("displayName"),self,self.columns[col].getAttribute("name")) + '" numFmtId="0">'
      		 	cacheDefinition = cacheDefinition + '<sharedItems count="'+self.conditions[col].distinctValues.length+'">'
        	 	for (var val = 0; val < self.conditions[col].distinctValues.length; val++){
        	 		cacheDefinition = cacheDefinition + '<s v="' + cleanDistinctValue(self.conditions[col].distinctValues[val], self)/*self.conditions[col].distinctValues[val].replace(/\&/g, "&amp;").replace(/"/g, "&quot;")*/ + '"/>';
        	 	}
      		 	cacheDefinition = cacheDefinition + '</sharedItems>'
    		 	cacheDefinition = cacheDefinition + '</cacheField>'
			}
			for (var iCV = 0; iCV < measures.length; iCV++) {
				cacheDefinition = cacheDefinition + '<cacheField name="' + cleanDistinctValue(measures[iCV].getAttribute("displayName"),self,measures[iCV].getAttribute("name")) + '" numFmtId="0">'
      			cacheDefinition = cacheDefinition + '<sharedItems/>'
    			cacheDefinition = cacheDefinition + '</cacheField>'
			}	
			cacheDefinition = cacheDefinition + '</cacheFields>'
			cacheDefinition = cacheDefinition + '</pivotCacheDefinition>'
		
			//xl - woorksheet - sheet2
			var sheet2 = '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>'
				+'<worksheet xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships">'
  				+'<dimension ref="A1:' + letras[self.columns.length + measures.length -1] + (dataToShow.length+1) + '"/>'
  				+'<sheetViews>'
    			+'<sheetView workbookViewId="0"/>'
  				+'</sheetViews>'
  				+'<sheetData>'
  			var rowNumber = 1;
  			var colNumber = 0; 		
  			//headers
  			sheet2 = sheet2 + '<row r="' + rowNumber + '" span="1:3">'
  			for(var col=0; col < self.columns.length; col++){
  				 sheet2 = sheet2 + '<c t="inlineStr" r="' + letras[colNumber] + rowNumber + '">'
				 sheet2 = sheet2 + '<is><t>' + cleanDistinctValue(self.columns[col].getAttribute("displayName"),self,self.columns[col].getAttribute("name")) + '</t></is>'
      			 sheet2 = sheet2 + '</c>' 
      			 colNumber++;
			}	
			for (var iCV = 0; iCV < measures.length; iCV++) {
				sheet2 = sheet2 + '<c t="inlineStr" r="' + letras[colNumber] + rowNumber + '">'
				sheet2 = sheet2 + '<is><t>' + cleanDistinctValue(measures[iCV].getAttribute("displayName"),self,measures[iCV].getAttribute("name")) + '</t></is>'
      			sheet2 = sheet2 + '</c>'
      			colNumber++;
			}		
			sheet2 = sheet2 + '</row>'
		
			for(var rN = 0; rN < dataToShow.length; rN++){
				colNumber=0;
				rowNumber++;
				sheet2 = sheet2 + '<row r="' + rowNumber + '" span="1:3">'
				for(var col=0; col < self.columns.length; col++){
					var	pos = self.conditions[col].dataRowPosition
					var value = cleanDistinctValue(dataToShow[rN][pos], self)
					
					sheet2 = sheet2 + '<c t="inlineStr" r="' + letras[colNumber] + rowNumber + '">'
			 		sheet2 = sheet2 + '<is><t>' + value + '</t></is>'
      		 		sheet2 = sheet2 + '</c>' 
      		 		colNumber++;
				}	
			
				for (var iCV = 0; iCV < measures.length; iCV++) {
					pos = self.columns.length + iCV;
					var value = self.defaultPictureValue(dataToShow[rN][pos], (iCV == measures.length-1), iCV) 
					value = removeComas(value, self);
					value = cleanDistinctValue(value);
					if ((measures[iCV].getAttribute('dataType') === "real") || (measures[iCV].getAttribute('dataType') === "integer")){
						sheet2 = sheet2 +  '<c r="'+ letras[colNumber] + rowNumber + '">'
        				sheet2 = sheet2 +  '<v>' + value + '</v>'
      					sheet2 = sheet2 +  '</c>' 
					} else {
						sheet2 = sheet2 + '<c t="inlineStr" r="' + letras[colNumber] + rowNumber + '">'
				 		sheet2 = sheet2 + '<is><t>' + value + '</t></is>'
      			 		sheet2 = sheet2 + '</c>' 
					}
					colNumber++;
				}
				sheet2 = sheet2 + '</row>'
			} 
		
			sheet2 = sheet2 + '</sheetData>'
			sheet2 = sheet2 + '</worksheet>'
		
			sheet = xlsx({
      				creator: 'Genexus',
      				lastModifiedBy: 'Genexus',
      				pivot: true, 
      				pivotCacheDefinition: cacheDefinition,
      				pivotTable1: xlpivotTable1,
      				Sheet2: sheet2,
      				style: xlstyle,
      				dfx: dfxStr,
      				worksheets: [{
        					data: dataTable,
        					name: 'Sheet 1'
      							}]
    					 });
		
			if ((gx.util.browser.webkit) && (!gx.util.browser.chrome)){ //for safari
					window.location = sheet.href();	
			} else if (!gx.util.browser.isIE() || (9<gx.util.browser.ieVersion())){												
				//window.location = sheet.href();
				var byteCharacters = atob(sheet.base64);
				function charCodeFromCharacter(c) {
    				return c.charCodeAt(0);
				}

				var byteNumbers = Array.prototype.map.call(byteCharacters,charCodeFromCharacter);
				var uint8Data = new Uint8Array(byteNumbers);
				
    			var blob = new Blob([uint8Data], {type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"});
            	saveAs( blob, fileName+".xlsx");
			} else {
				return sheet.base64;	
			}
		/*if (!gx.util.browser.isIE()){
			window.location = sheet.href();
		} else {
			return sheet.base64; //sheet.base64;		
		}*/
		
		} else {
			
			//TODO: Quitar
			//self.formulaInfo.cantFormulaMeasures = 1
			//self.formulaInfo.measureFormula[2].hasFormula = true
			//TODO: Quitar
			
			//xl/pivotTables/pivotTable1.xml string definition
			var firstDataRow = self.colConditions.length + 1;
			var firstDataCol = (((self.colConditions.length > 0)) ? self.colConditions.length : 1); 
			if ((self.filterIndexes.length > 0) && (self.rowConditions.length+1 == measures.length)){
				firstDataCol = 0;
			}   
		
			if ((self.rowConditions.length + self.filterIndexes.length) == 0){
				firstDataRow = 1;
				firstDataCol = 1;
			}
			
			var varRef = "1"
			var pageInfo = ""
			if (self.filterIndexes.length > 0){
				firstDataRow =  firstDataRow + self.filterIndexes.length;//<-- nuevo
				pageInfo = " rowPageCount=\""+self.filterIndexes.length+"'\" colPageCount=\"1\" "
				varRef = firstDataRow + 1 
			}
			
			var rowLabel = "";
			if (self.rowConditions.length > 0){
				var firstRow = self.rowConditions[0]
				if (self.columns.length > firstRow){
					rowLabel = ' rowHeaderCaption="' +cleanDistinctValue(self.columns[firstRow].getAttribute("displayName"),self,self.columns[firstRow].getAttribute("name")) + '" ';
				} 
			} 
			if (self.colConditions.length > 0){
				var firstCol = self.colConditions[0]
				if (self.columns.length > firstCol){
					rowLabel = rowLabel + ' colHeaderCaption="' +cleanDistinctValue(self.columns[firstCol].getAttribute("displayName"),self,self.columns[firstCol].getAttribute("name")) + '" ';
				}
			} 
			var xlpivotTable1 = '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>\n'
							+'<pivotTableDefinition name="Tabla dinamica1" cacheId="5" dataCaption="'+gx.getMessage("GXPL_QViewerJSValue")+'" createdVersion="3" updatedVersion="3" minRefreshableVersion="3" applyNumberFormats="0" applyBorderFormats="0" applyFontFormats="0" applyPatternFormats="0" applyAlignmentFormats="0" applyWidthHeightFormats="1" showCalcMbrs="0" useAutoFormatting="1" itemPrintTitles="1" indent="0" outline="1" outlineData="1" multipleFieldFilters="0" '
							//+' rowHeaderCaption="' + rowLabel + '" ' + totalsExportPivotPdfOptions + ' xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main">\n'
							+ rowLabel + ' ' + totalsExportPivotPdfOptions + ' xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main">\n' 
							+"<location "
							+"ref=\"A" + varRef + "\""//ref=\"A1\"' <-- nuevo
							+" firstHeaderRow=\"1\" firstDataRow=\""+ firstDataRow +"\" firstDataCol=\""+ firstDataCol +"\" "
							+ pageInfo//<-- nuevo
							+"/>\n"
													+ "<pivotFields>\n"
			var pfi = [];
			var pfvi = [];	
			var pfviExclude = [];	
			
			if (self.formulaInfo.cantFormulaMeasures == 0){									
				for (var iCV = 0; iCV < measures.length; iCV++) {
					var numFmt = 163
					if (measures[iCV].getAttribute('picture') != ""){
						numFmt = 164 + iCV
					}
					xlpivotTable1 = xlpivotTable1 + "<pivotField dataField=\"1\" showAll=\"0\"  numFmtId=\"" + numFmt + "\"/>\n";
					
					pfi.push(cleanDistinctValue(measures[iCV].getAttribute("displayName"),self,measures[iCV].getAttribute("name")));
				}											
			}
		
			var rowLevel = -1;
			var lastRowLevel = 0; 
			var colLevel = -1; 
			var lastColLevel = 0;
			
			//C-L
			for(var filter=0; filter < self.filterIndexes.length; filter++){
				var iCV = self.filterIndexes[filter];
			
				position = 'axisPage';	
				
				var formulaOptionString = 'numFmtId="163"';
    			var formulaItemString = "<item t=\"default\"/>\n"
    			//if (hasFormula){
    				//formulaOptionString = 'defaultSubtotal=\"0\"'
    				//formulaItemString = ""
    			//}  
    			
    			xlpivotTable1 = xlpivotTable1 + "<pivotField axis=\"" + position + "\" showAll=\"0\" " + formulaOptionString + " >\n"
    								+"<items>\n"
    			var pfvl; var pfvlExclude;
    			if (self.conditions[iCV].distinctValues != undefined){
    				pfvl = []; pfvlExclude = [];
    				var distValues = self.conditions[iCV].distinctValues
    				//if (position == 'axisPage'){
    					//distValues = self.GeneralDistinctValues[iCV]
    				//}
    				for (var val = 0; val < distValues.length; val++){
    					pfvl.push( cleanDistinctValue(distValues[val], self));
    					var hiddenString = "";
    					if (self.conditions[iCV].blackList.find(distValues[val]) != -1){
    						hiddenString = " h=\"1\"";
    						pfvlExclude.push(cleanDistinctValue(distValues[val], self));
    					}
    					xlpivotTable1 = xlpivotTable1 + "<item x=\""+ val +"\" "+ hiddenString +" />\n";
    				} 
    				xlpivotTable1 = xlpivotTable1 + formulaItemString /*+ "<item t=\"default\"/>"*/
    								+ "</items>\n"
    								+ "</pivotField>\n";
    			}
    			pfvi[iCV] = pfvl;
    			pfviExclude[iCV] = pfvlExclude[iCV]
    			pfi.push(iCV);
			}
			//C-L
			
			for(var iCV=0; iCV < self.columns.length; iCV++){
    			if (self.filterIndexes.indexOf(iCV) == -1){ //C-L
    			var position = 'axisRow';
    			if ((self.rowConditions.find(iCV)!=-1) || ( ((self.rowConditions.length + self.filterIndexes.length) == 0) ) ){ 
    				position = 'axisRow';
    				rowLevel++;
    				lastRowLevel = iCV;
    			} else if (self.colConditions.find(iCV)!=-1) {
    				position = 'axisCol';
    				colLevel++;
    				lastColLevel = iCV;
    			} else {
    				position = 'axisPage';		//<-- nuevo
    				//rowLevel++;
    				//lastRowLevel = iCV;
    			}
    			var formulaOptionString = 'numFmtId="163"';
    			var formulaItemString = "<item t=\"default\"/>\n"
    			//if (hasFormula){
    				//formulaOptionString = 'defaultSubtotal=\"0\"'
    				//formulaItemString = ""
    			//}  
    			
    			xlpivotTable1 = xlpivotTable1 + "<pivotField axis=\"" + position + "\" showAll=\"0\" " + formulaOptionString + " >\n"
    								+"<items>\n"
    			var pfvl; var pfvlExclude;
    			if (self.conditions[iCV].distinctValues != undefined){
    				pfvl = []; pfvlExclude = [];
    				var distValues = self.conditions[iCV].distinctValues
    				//if (position == 'axisPage'){
    					//distValues = self.GeneralDistinctValues[iCV]
    				//}
    				for (var val = 0; val < distValues.length; val++){
    					pfvl.push( cleanDistinctValue(distValues[val], self));
    					var hiddenString = "";
    					if (self.conditions[iCV].blackList.find(distValues[val]) != -1){
    						hiddenString = " h=\"1\"";
    						pfvlExclude.push(cleanDistinctValue(distValues[val], self));
    					}
    					xlpivotTable1 = xlpivotTable1 + "<item x=\""+ val +"\" "+ hiddenString +" />";
    				} 
    				xlpivotTable1 = xlpivotTable1 + formulaItemString /*+ "<item t=\"default\"/>"*/
    								+ "</items>\n"
    								+ "</pivotField>\n";
    			}
    			pfvi[iCV] = pfvl;
    			pfviExclude[iCV] = pfvlExclude[iCV]
    			pfi.push(iCV);	
    			}
    		}
    		
    		
    		if (self.formulaInfo.cantFormulaMeasures > 0){									
				for (var iCV = 0; iCV < measures.length; iCV++) { //first normal measures
					var numFmt = 163
					if (measures[iCV].getAttribute('picture') != ""){
						numFmt = 164 + iCV
					}
					if (self.formulaInfo.measureFormula[iCV].hasFormula){
						//xlpivotTable1 = xlpivotTable1 + '<pivotField dataField="1" dragToRow="0" dragToCol="0" dragToPage="0" showAll="0" defaultSubtotal="0" numFmtId="' + numFmt + '"/>\n'
					} else { 
						
						xlpivotTable1 = xlpivotTable1 + "<pivotField dataField=\"1\" showAll=\"0\"  numFmtId=\"" + numFmt + "\"/>\n";
					
						pfi.push(cleanDistinctValue(measures[iCV].getAttribute("displayName"),self,measures[iCV].getAttribute("name")));
					}
				}
				
				for (var iCV = 0; iCV < formMeasures.length; iCV++){//add hidden measures for formula
					xlpivotTable1 = xlpivotTable1 + '<pivotField showAll="0" defaultSubtotal="0"/>\n'
				}
				
				for (var iCV = 0; iCV < measures.length; iCV++) { //second formula measures
					if (self.formulaInfo.measureFormula[iCV].hasFormula){
						xlpivotTable1 = xlpivotTable1 + '<pivotField dataField="1" dragToRow="0" dragToCol="0" dragToPage="0" showAll="0" defaultSubtotal="0" numFmtId="' + numFmt + '"/>\n'
					}											
				}
			}
    		
    		
    		xlpivotTable1 = xlpivotTable1 + "</pivotFields>\n"
    		
    		if ((self.rowConditions.length+1 != measures.length)){
    			xlpivotTable1 = xlpivotTable1 + "<rowFields>\n"
				for(var iCV=0; iCV < self.columns.length; iCV++){
					if ((self.rowConditions.find(iCV)!=-1) /*|| (self.filterIndexes.find(iCV)!=-1)*/ //<-- cambio
						|| ( ((self.rowConditions.length + self.filterIndexes.length) == 0)) 
					){
						var pos = pfi.indexOf(iCV);
						xlpivotTable1 = xlpivotTable1 + "<field x=\"" + pos + "\"/>\n";
					}
				}
				xlpivotTable1 = xlpivotTable1 + "</rowFields>\n"
			} 
		
		
			xlpivotTable1 = xlpivotTable1 + "<rowItems>\n"
			for (var it = 0; it < self.rowStructure.items.length; it++){
    				xlpivotTable1 = xlpivotTable1 + createExcelROWITEMS(self.rowStructure.items[it], rowLevel, pfvi, lastRowLevel, rowLevel)
			}
		
			xlpivotTable1 = xlpivotTable1 + "<i t=\"grand\"><x/></i>\n"
									  + "</rowItems>\n"
		
			if ((((colLevel > -1)) || ((measures.length > 1)))){
				xlpivotTable1 = xlpivotTable1 + "<colFields>\n"
		
				for(var iCV=0; iCV < self.columns.length; iCV++){
					if ((self.colConditions.find(iCV)!=-1) && ( (((self.rowConditions.length + self.filterIndexes.length) != 0)))){ 
						var pos = pfi.indexOf(iCV);
						xlpivotTable1 = xlpivotTable1 + "<field x=\"" + pos + "\"/>\n";
					}
		 		}
		 		if (measures.length > 1){
                	xlpivotTable1 = xlpivotTable1 + "<field x=\"-2\"/>\n";
         		};
    			//<field x="-2"/>
  				xlpivotTable1 = xlpivotTable1 + "</colFields>\n"				
			}		
		
			//col items
			xlpivotTable1 = xlpivotTable1 + "<colItems>\n"
			if ((self.colStructure.items) && ((self.rowConditions.length + self.filterIndexes.length) != 0) ){ 
				for (var it = 0; it < self.colStructure.items.length; it++){
    				xlpivotTable1 = xlpivotTable1 + createExcelCOLITEMS(self.colStructure.items[it], colLevel, pfvi, lastColLevel, colLevel)
    			}				
			} else {
				for (var iCV = 0; iCV < measures.length; iCV++) {
					xlpivotTable1 = xlpivotTable1 + '<i/>\n';
				}
			}
			xlpivotTable1 = xlpivotTable1 + "</colItems>\n"
		
			if (self.filterIndexes.length > 0){
				xlpivotTable1 = xlpivotTable1 + "<pageFields>\n"
				for( var find = 0; find < self.filterIndexes.length; find++){
					xlpivotTable1 =	xlpivotTable1 + "<pageField fld=\""+(measures.length+find)+"\" hier=\"-1\"/>\n" //C-L +find
				}
				xlpivotTable1 =	xlpivotTable1 + "</pageFields>\n"
			}
		
			xlpivotTable1 = xlpivotTable1 + "<dataFields>\n"
    		for (var iCV = 0; iCV < measures.length; iCV++) {
    			var numFmt = 163
				if (measures[iCV].getAttribute('picture') != ""){
					numFmt = 164 + iCV
				}
    			if (!self.formulaInfo.measureFormula[iCV].hasFormula){
    				var addWhenFormula = 0;
					if (self.formulaInfo.cantFormulaMeasures > 0) addWhenFormula = self.columns.length
					var space = "";
					if (measures[iCV].getAttribute("displayName").indexOf("%")>-1){
						for (var t=0; t < iCV; t++){
						 	space = space + " ";
						}
						xlpivotTable1 = xlpivotTable1 + "<dataField name=\""+ measures[iCV].getAttribute("displayName") + space +"\" fld=\"" + (iCV + addWhenFormula) + "\" subtotal=\"sum\" numFmtId=\"" + numFmt + "\"/>\n"
					} else {
						xlpivotTable1 = xlpivotTable1 + "<dataField name=\""+ cleanDistinctValue(measures[iCV].getAttribute("displayName"),self,measures[iCV].getAttribute("name")) +"\" fld=\"" + (iCV + addWhenFormula) + "\" subtotal=\"sum\" numFmtId=\"" + numFmt + "\"/>\n"
					}
				}// else {
					//xlpivotTable1 = xlpivotTable1 + "<dataField name=\""+ measures[iCV].getAttribute("displayName") +" \" fld=\"" + (iCV + self.columns.length) + "\" numFmtId=\"" + numFmt + "\" baseField=\"0\" baseItem=\"0\"/>\n"
				//}
    			
    		}
    		
    		for (var iCV = 0; iCV < measures.length; iCV++) {
    			var numFmt = 163
				if (measures[iCV].getAttribute('picture') != ""){
					numFmt = 164 + iCV
				}
				if (self.formulaInfo.measureFormula[iCV].hasFormula){
					if (measures[iCV].getAttribute("displayName").indexOf("%")>-1){
						for (var t=0; t < iCV; t++){
						 	space = space + " ";
						}
						xlpivotTable1 = xlpivotTable1 + "<dataField name=\""+ measures[iCV].getAttribute("displayName") + space +" \" fld=\"" + (iCV + self.columns.length + formMeasures.length) + "\" numFmtId=\"" + numFmt + "\" baseField=\"0\" baseItem=\"0\"/>\n"
					} else {
						xlpivotTable1 = xlpivotTable1 + "<dataField name=\""+ cleanDistinctValue(measures[iCV].getAttribute("displayName"),self,measures[iCV].getAttribute("name")) +" \" fld=\"" + (iCV + self.columns.length + formMeasures.length) + "\" numFmtId=\"" + numFmt + "\" baseField=\"0\" baseItem=\"0\"/>\n"
					}
				} 
    		}
    		
  			xlpivotTable1 =	xlpivotTable1 + "</dataFields>\n"
	  			+ "<pivotTableStyleInfo name=\"PivotStyleLight16\" showRowHeaders=\"1\" showColHeaders=\"1\" showRowStripes=\"0\" showColStripes=\"0\" showLastColumn=\"1\"/>\n"
	  			+ "</pivotTableDefinition>\n"
		
		
			//xl/pivotCache/cacheRecords
			var cacheRecords = '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>\n' 
						+ '<pivotCacheRecords xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships">'
			for(var rN = 0; rN < dataToShow.length; rN++){
				cacheRecords = 	cacheRecords + "<r>"
			
				for(var col=0; col < self.columns.length; col++){
					var vvalue = pfvi[col].indexOf( cleanDistinctValue(dataToShow[rN][col], self) )//self.GeneralDataRows[rN][col].replace(/\&/g, "&amp;").replace(/"/g, "&quot;"))
					cacheRecords = 	cacheRecords + '<x v="' + vvalue + '"/>'
				}
			
				for (var iCV = 0; iCV < measures.length; iCV++) {
					var	pos = self.columns.length + iCV;
				
					var value = self.defaultPictureValue(dataToShow[rN][pos], (iCV == measures.length-1), iCV) 
					value = removeComas(value, self);
					value = cleanDistinctValue(value, self)//value.replace(/\&/g, "&amp;").replace(/"/g, "&quot;")
					cacheRecords = 	cacheRecords + '<n v=\"' + value + '"/>';
				
				}
				cacheRecords = 	cacheRecords + "</r>" 	
			}
			cacheRecords = cacheRecords + '</pivotCacheRecords>'; 
		
		
			//xl/pivotCache/cacheDefinition
			var cacheDefinition = '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>\n'
							+ '<pivotCacheDefinition r:id="rId1" refreshOnLoad="1" xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships">\n'
  							+ '<cacheSource type="worksheet">\n'
  			var letras = ['A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z','AA','AB','AC','AD'];					
			cacheDefinition = cacheDefinition + '<worksheetSource ref="A1:' + letras[self.columns.length + measures.length -1 - self.formulaInfo.cantFormulaMeasures + formMeasures.length] + (dataToShow.length+1) + '" sheet="SourceData"/>\n'
			cacheDefinition = cacheDefinition + '</cacheSource>\n'
			cacheDefinition = cacheDefinition + '<cacheFields count="' + (self.columns.length + measures.length + formMeasures.length) + '">\n'
			if (self.formulaInfo.cantFormulaMeasures == 0){
				for (var iCV = 0; iCV < measures.length; iCV++) {
					cacheDefinition = cacheDefinition + '<cacheField name="' + cleanDistinctValue(measures[iCV].getAttribute("displayName"),self,measures[iCV].getAttribute("name")) + '" numFmtId="0">\n'
      				cacheDefinition = cacheDefinition + '<sharedItems/>\n'
    				cacheDefinition = cacheDefinition + '</cacheField>\n'
				}
			}
			
			//C-L
			for(var filter=0; filter < self.filterIndexes.length; filter++){
				var col = self.filterIndexes[filter];
				
				//containmixed
				var mixed = "";
				for (var val = 0; val < self.GeneralDistinctValues[col].length; val++){
					if (!isNaN(self.GeneralDistinctValues[col][val])){
						mixed = ' containsMixedTypes="1" containsString="1" containsNumber="1" containsInteger="1" ';
						break;
					}
				}
				if (mixed != ""){
					var onlyNumber = true;
					for (var val = 0; val < self.GeneralDistinctValues[col].length; val++){
						if (isNaN(self.GeneralDistinctValues[col][val])){
							onlyNumber = false;
							break;
						}
					}	
					if (onlyNumber){
						mixed = ' containsSemiMixedTypes="0" containsString="0" containsNumber="1" containsInteger="1" ';
					}
				}
				
				cacheDefinition = cacheDefinition + '<cacheField name="' + cleanDistinctValue(self.columns[col].getAttribute("displayName"),self,self.columns[col].getAttribute("name")) + '" numFmtId="0">\n'
      		 	cacheDefinition = cacheDefinition + '<sharedItems ' + mixed + ' count="'+self.GeneralDistinctValues[col].length+'">\n'
        	 	for (var val = 0; val < self.GeneralDistinctValues[col].length; val++){
        	 		if (!isNaN(self.GeneralDistinctValues[col][val])){
        	 			cacheDefinition = cacheDefinition + '<n v="' + cleanDistinctValue(self.GeneralDistinctValues[col][val], self) + '"/>\n';
        	 		} else {
        	 			cacheDefinition = cacheDefinition + '<s v="' + cleanDistinctValue(self.GeneralDistinctValues[col][val], self) + '"/>\n';
        	 		} 
        	 	}
      		 	cacheDefinition = cacheDefinition + '</sharedItems>\n'
    		 	cacheDefinition = cacheDefinition + '</cacheField>\n'
			} 
			//C-L
			
			for(var col=0; col < self.columns.length; col++){
				if (self.filterIndexes.indexOf(col) == -1){ //C-L
					cacheDefinition = cacheDefinition + '<cacheField name="' + cleanDistinctValue(self.columns[col].getAttribute("displayName"),self,self.columns[col].getAttribute("name")) + '" numFmtId="0">\n'
      		 		cacheDefinition = cacheDefinition + '<sharedItems count="'+self.conditions[col].distinctValues.length+'">\n'
        	 		for (var val = 0; val < self.conditions[col].distinctValues.length; val++){
        	 			cacheDefinition = cacheDefinition + '<s v="' + cleanDistinctValue(self.conditions[col].distinctValues[val], self)/*self.conditions[col].distinctValues[val].replace(/\&/g, "&amp;").replace(/"/g, "&quot;")*/ + '"/>\n'; 
        	 		}
      		 		cacheDefinition = cacheDefinition + '</sharedItems>\n'
    		 		cacheDefinition = cacheDefinition + '</cacheField>\n'
    		    } //C-L
			}
			
			if (self.formulaInfo.cantFormulaMeasures > 0){
				for (var iCV = 0; iCV < measures.length; iCV++) {
					if (!self.formulaInfo.measureFormula[iCV].hasFormula){  //normal measures
						cacheDefinition = cacheDefinition + '<cacheField name="' + cleanDistinctValue(measures[iCV].getAttribute("displayName"),self,measures[iCV].getAttribute("name")) + '" numFmtId="165">\n'
      					cacheDefinition = cacheDefinition + '<sharedItems/>\n'
    					cacheDefinition = cacheDefinition + '</cacheField>\n'
    				}// else {
    					//cacheDefinition = cacheDefinition + '<cacheField name="' + measures[iCV].getAttribute("displayName") + '" numFmtId="165" formula="\'Vendido\'/\'Planificado\'" databaseField="0"/>\n'	
    				//}
				}
				
				
				
				for (var iCV = 0; iCV < formMeasures.length; iCV++){//add hidden measures for formula
					cacheDefinition = cacheDefinition + '<cacheField name="'+ getMeasureIdentifier(self, formMeasures[iCV]) +'" numFmtId="0">\n'+
					//<sharedItems containsSemiMixedTypes="0" containsString="0" containsNumber="1" containsInteger="1" minValue="100" maxValue="1100"/>
					'</cacheField>\n'
				}
				
				
				for (var iCV = 0; iCV < measures.length; iCV++) { //formula measures
					if (self.formulaInfo.measureFormula[iCV].hasFormula){
						cacheDefinition = cacheDefinition + '<cacheField name="' + cleanDistinctValue(measures[iCV].getAttribute("displayName"),self,measures[iCV].getAttribute("name")) + '" numFmtId="165" formula="'+self.formulaInfo.measureFormula[iCV].textFormula+'" databaseField="0"/>\n'
					}
				}
			}
			
			cacheDefinition = cacheDefinition + '</cacheFields>\n'
			cacheDefinition = cacheDefinition + '</pivotCacheDefinition>\n'
			
			//xl - woorksheet - sheet2
			var sheet2 = '<?xml version="1.0" encoding="UTF-8" standalone="yes"?>'
				+'<worksheet xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships">'
  				+'<dimension ref="A1:' + letras[self.columns.length + measures.length -1] + (dataToShow.length+1) + '"/>'
  				+'<sheetViews>'
    			+'<sheetView workbookViewId="0"/>'
  				+'</sheetViews>'
  				+'<sheetData>'
  			var rowNumber = 1;
  			var colNumber = 0; 	
  			//headers
  			sheet2 = sheet2 + '<row r="' + rowNumber + '" span="1:3">'
  			for(var col=0; col < self.columns.length; col++){
  				 sheet2 = sheet2 + '<c t="inlineStr" r="' + letras[colNumber] + rowNumber + '">'
				 sheet2 = sheet2 + '<is><t>' + cleanDistinctValue(self.columns[col].getAttribute("displayName"), self,self.columns[col].getAttribute("name")).trim() + '</t></is>'
      			 sheet2 = sheet2 + '</c>' 
      			 colNumber++;
			}
			for (var iCV = 0; iCV < measures.length; iCV++) {
				sheet2 = sheet2 + '<c t="inlineStr" r="' + letras[colNumber] + rowNumber + '">'
				sheet2 = sheet2 + '<is><t>' + cleanDistinctValue(measures[iCV].getAttribute("displayName"), self,measures[iCV].getAttribute("name")).trim() + '</t></is>'
      			sheet2 = sheet2 + '</c>'
      			colNumber++;
			}	
			sheet2 = sheet2 + '</row>'
		
			sheet2 = sheet2 + '</sheetData>'
			sheet2 = sheet2 + '</worksheet>'
	
			
			//unicamente la seccion de las numberFormat
			//conversion a hacer letras P prefijas se cambian por \P la primera por \\P las demas
			//Z se cambia por #
			//, y . quedan por , y .
			//9 se cambia por 0
			
			var xlstyle = '<numFmts>'
			var addNumberFormat = false
			for (var mPos = 0; mPos < measures.length; mPos++){
				if (measures[mPos].getAttribute('picture') != ""){
					var pivotFormat = measures[mPos].getAttribute('picture') 
					var numFmt = 164 + mPos
					
					var excelFormat = ""
					for (var charFP = 0; charFP < pivotFormat.length; charFP++){
						if (pivotFormat[charFP] == 'Z'){
							excelFormat = excelFormat + "#"
						} else if (pivotFormat[charFP] == '9'){
							excelFormat = excelFormat + "0"
						} else if ((pivotFormat[charFP] == '.') || (pivotFormat[charFP] == ',')){
							excelFormat = excelFormat + pivotFormat[charFP]
						} else /*if ((charFP == 0))*/{
							excelFormat = excelFormat + "\\" + pivotFormat[charFP]
						} //else {
						
						//}
					}
					
					//var excelFormat = "###,###.00"
					//xlstyle = xlstyle + '<numFmt formatCode="' + excelFormat + '" numFmtId="' + numFmt + '"/>'
					xlstyle = xlstyle + '<numFmt formatCode="' + excelFormat + '" numFmtId="' + numFmt + '"/>'
					addNumberFormat = true
				}
			}
			if (addNumberFormat) {
				xlstyle = xlstyle + '</numFmts>'
			} else {
				xlstyle = ''
			}
		
			sheet = xlsx({
      				creator: 'Genexus',
      				lastModifiedBy: 'Genexus',
      				pivot: true, 
      				pivotCacheDefinition: cacheDefinition,
      				pivotTable1: xlpivotTable1,
      				Sheet2: "",//sheet2,
      				style: xlstyle,
      				worksheets: [{
        					data: dataTable,
        					name: 'Sheet 1'
      							}]
    					 });
    					 
    						 
    					 
			
		}
		
		if ((gx.util.browser.webkit) && (!gx.util.browser.chrome)){ //for safari
				window.location = sheet.href();	
		} else if (!gx.util.browser.isIE() || (9<gx.util.browser.ieVersion())){												
				//window.location = sheet.href();
				var byteCharacters = atob(sheet.base64);
				function charCodeFromCharacter(c) {
    				return c.charCodeAt(0);
				}

				var byteNumbers = Array.prototype.map.call(byteCharacters,charCodeFromCharacter);
				var uint8Data = new Uint8Array(byteNumbers);
				
    			var blob = new Blob([uint8Data], {type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"});
            	saveAs( blob, fileName+".xlsx");
		} else {
				return sheet.base64;	
		}
		
		
    	
	}

}



